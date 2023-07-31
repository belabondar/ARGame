using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class buttonUI : MonoBehaviour
{

    private GameObject _fighter, _vampire, _mage;
    public Button punchButton;
    public Button magicButton;

    public GameObject prefabParent;

    private Transform[] _parentTransforms;
    private bool _showUi;
    private List<GameObject> _prefabs = new List<GameObject>();


    void Start() {
        punchButton.onClick.AddListener(OnPunchBtnClick);
        magicButton.onClick.AddListener(OnMagicBtnClick);

        _parentTransforms = prefabParent.GetComponentsInChildren<Transform>(true);
        foreach(Transform t in prefabParent.transform){
            _prefabs.Add(t.gameObject);
        }

        foreach (var prefab in _prefabs)
        {
            Debug.Log(prefab);
        }
    }

     private void Update()
     {
         SetShowUi();
         punchButton.gameObject.SetActive(_showUi);
         magicButton.gameObject.SetActive(_showUi);
     }

     private void SetShowUi()
     {
         //Show UI only if a prefab is active
         foreach(var g in _prefabs){
             if (g.activeSelf)
             {
                 _showUi = true;
                 return;
             }
         }

         _showUi = false;
     }

     private void OnPunchBtnClick() {
        PlayPunchAnimation();
    }

    private void OnMagicBtnClick() {
        PlayMagicAnimation();
    }

    // Call this method from the first button's OnClick event
    private void PlayPunchAnimation()
    {
        var activeAnimators = GetActiveAnimators();
        foreach (var animator in activeAnimators)
        {
                Debug.Log("exist");
                if (animator != null)
                {
                    animator.SetTrigger("TrPunch");
                }
                
        }
    }

    // Call this method from the second button's OnClick event
    private void PlayMagicAnimation()
    {
        var activeAnimators = GetActiveAnimators();
        foreach (var animator in activeAnimators)
        {
            Debug.Log("exist");
            if (animator != null)
            {
                animator.SetTrigger("TrMagic");
            }
        }
    }

    private List<Animator> GetActiveAnimators()
    {
        List<Animator> animators = new List<Animator>();

        foreach (var prefab in _prefabs)
        {
            if (prefab.activeSelf)
            {
                animators.Add(prefab.GetComponent<Animator>());
            }
        }

        return animators;
    }
}
