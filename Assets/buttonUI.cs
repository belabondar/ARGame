using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class buttonUI : MonoBehaviour
{

    public Animator fighterAnimController;
    public Button firstB;
    public Button secondB;
    public GameObject fighterObj;

    // Set these parameters in the Unity Inspector
    public string firstAnimationParam = "magic";
    public string secondAnimationParam = "punch";

     void Start() {
        Debug.Log("ASDASd");
        firstB.onClick.AddListener(OnButtonClick);
        secondB.onClick.AddListener(OnSecondButtonClick);

        GameObject wwwtObject = GameObject.Find("fighter");

        
        fighterAnimController = wwwtObject.GetComponent<Animator>();
         
    }

    public void OnButtonClick() {
        PlayFirstAnimation();
    }

    public void OnSecondButtonClick() {
        PlaySecondAnimation();
    }

    // Call this method from the first button's OnClick event
    public void PlayFirstAnimation()
    {
        Debug.Log("Called");
        // Ensure the animator exists
        if (fighterAnimController != null)
        {
            Debug.Log("exist");
            // Set the first animation boolean parameter to true
            fighterAnimController.SetTrigger("magicc");
        }
    }

    // Call this method from the second button's OnClick event
    public void PlaySecondAnimation()
    {
        // Ensure the animator exists
        if (fighterAnimController != null)
        {
            // Set the first animation boolean parameter to false
            fighterAnimController.SetTrigger("punnch");
        }
    }

   
}
