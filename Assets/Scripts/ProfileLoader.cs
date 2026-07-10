using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProfileLoader : MonoBehaviour
{
    // Start is called before the first frame update
    [Header("Add Objects Here")]

    public GameObject gbcProfile;
    public GameObject clcProfile;
    public GameObject swcProfile;

    public AudioClip clickSFX;
    void Start()
    {
        gbcProfile.SetActive(false);
        clcProfile.SetActive(false);
        swcProfile.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OpenGBC() { gbcProfile.SetActive(true); AudioManager.Instance.PlaySFX(clickSFX); }
    public void CloseGBC() { gbcProfile.SetActive(false); }

    public void OpenCLC() { clcProfile.SetActive(true); AudioManager.Instance.PlaySFX(clickSFX); }
    public void CloseCLC() { clcProfile.SetActive(false); }

    public void OpenSWC() { swcProfile.SetActive(true); AudioManager.Instance.PlaySFX(clickSFX); }
    public void CloseSWC() { swcProfile.SetActive(false); }

    public void CloseAll()
    {
        AudioManager.Instance.PlaySFX(clickSFX);
        gbcProfile.SetActive(false);
        clcProfile.SetActive(false);
        swcProfile.SetActive(false);
    }

    public void PlaySFX()
    {
        AudioManager.Instance.PlaySFX(clickSFX);
    }
}
