using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.ARFoundation;

[RequireComponent(typeof(ARTrackedImageManager))]
public class ImageRecognition : MonoBehaviour
{
private ARTrackedImageManager aRTrackedImageManager;

[SerializeField]
private GameObject[] placeablePrefabs;
private Dictionary<string, GameObject> spawnedPrefabs = new Dictionary<string, GameObject>();

public GameObject prefabParent;

private void Awake()
{
    aRTrackedImageManager = FindObjectOfType<ARTrackedImageManager>();

    foreach (GameObject prefab in placeablePrefabs)
    {
        Vector3 position = Vector3.zero;

        GameObject newPrefab = Instantiate(prefab, position, Quaternion.identity);
        newPrefab.name = prefab.name;
        newPrefab.SetActive(false);
        spawnedPrefabs.Add(prefab.name, newPrefab);
        newPrefab.transform.parent = prefabParent.transform;
    }
}

public void OnEnable()
{
    aRTrackedImageManager.trackedImagesChanged += OnImageChanged;
}

public void OnDisable()
{
    aRTrackedImageManager.trackedImagesChanged -= OnImageChanged;
}

public void OnImageChanged(ARTrackedImagesChangedEventArgs args)
{
    foreach(ARTrackedImage trackedImage in args.added) {
        UpdateImage(trackedImage);
        spawnedPrefabs[trackedImage.name].SetActive(true);
    }
    foreach(ARTrackedImage trackedImage in args.updated) {
        UpdateImage(trackedImage);
    }
    foreach(ARTrackedImage trackedImage in args.removed) {
        spawnedPrefabs[trackedImage.name].SetActive(false);
    }
}

private void UpdateImage(ARTrackedImage trackedImage) {

    string name = trackedImage.referenceImage.name;
    Vector3 position = trackedImage.transform.position;
    Quaternion targetRotation = Quaternion.Euler(-0f, 180f, 0f);

    foreach(GameObject go in spawnedPrefabs.Values) {
        if(go.name == name) {
            go.transform.position = position;
            go.transform.rotation = targetRotation;
            go.SetActive(true);
        }
    }

}

}
