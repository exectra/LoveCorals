using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class SpriteSwap : MonoBehaviour
{
    [Header("Assign the two Image GameObjects")]
    public GameObject imageA;
    public GameObject imageB;

    private bool showingA = true;

    void Start()
    {
        // Make sure only Image A is visible at the start
        imageA.SetActive(true);
        imageB.SetActive(false);
    }

    public void SwapImage()
    {
        showingA = !showingA;

        imageA.SetActive(showingA);
        imageB.SetActive(!showingA);
    }
}
