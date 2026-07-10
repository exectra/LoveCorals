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

    public void OpenGBC() { gbcProfile.SetActive(true); }
    public void CloseGBC() { gbcProfile.SetActive(false); }

    public void OpenCLC() { clcProfile.SetActive(true); }
    public void CloseCLC() { clcProfile.SetActive(false); }

    public void OpenSWC() { swcProfile.SetActive(true); }
    public void CloseSWC() { swcProfile.SetActive(false); }

    public void CloseAll()
    {
        gbcProfile.SetActive(false);
        clcProfile.SetActive(false);
        swcProfile.SetActive(false);
    }
}
