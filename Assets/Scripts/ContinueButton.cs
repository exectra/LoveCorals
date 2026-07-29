using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContinueButton : MonoBehaviour
{
    [Header("GameObject to activate when Continue is pressed")]
    public GameObject targetObject;

    [Header("Optional: GameObject to hide when Continue is pressed")]
    public GameObject currentObject;

    public void OnContinuePressed()
    {
        if (targetObject != null)
            targetObject.SetActive(true);

        if (currentObject != null)
            currentObject.SetActive(false);
    }
}
