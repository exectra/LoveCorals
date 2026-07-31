using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ProfileLoader : MonoBehaviour
{
    // Start is called before the first frame update
    [Header("Add Objects Here")]

    public GameObject gbcProfile;
    public GameObject clcProfile;
    public GameObject swcProfile;
    public GameObject identifyBtn;
    public SceneManager sceneManager;
    [SerializeField] string identifyScene = "(Main Menu) IdentifyScene 2.0";


    public AudioClip clickSFX;

    private void Awake()
    {
        //sceneManager = GameObject.Find("GameManager").GetComponent<SceneManager>(); ;
    }
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

    public void OpenGBC()
    {
        gbcProfile.SetActive(true); AudioManager.Instance.PlaySFX(clickSFX);
        PlayerPrefs.SetInt("ProfileEnabled", 1); PlayerPrefs.Save();
    }
    public void CloseGBC()
    {
        gbcProfile.SetActive(false);
        PlayerPrefs.SetInt("ProfileEnabled", 0); PlayerPrefs.Save();
    }

    public void OpenCLC()
    {
        clcProfile.SetActive(true); AudioManager.Instance.PlaySFX(clickSFX);
        PlayerPrefs.SetInt("ProfileEnabled", 1); PlayerPrefs.Save();
    }
    public void CloseCLC()
    {
        clcProfile.SetActive(false);
        PlayerPrefs.SetInt("ProfileEnabled", 0); PlayerPrefs.Save();
    }

    public void OpenSWC()
    {
        swcProfile.SetActive(true); AudioManager.Instance.PlaySFX(clickSFX);
        PlayerPrefs.SetInt("ProfileEnabled", 1); PlayerPrefs.Save();
    }
    public void CloseSWC()
    {
        swcProfile.SetActive(false);
        PlayerPrefs.SetInt("ProfileEnabled", 0); PlayerPrefs.Save();
    }

    public void CloseAll()
    {
        AudioManager.Instance.PlaySFX(clickSFX);
        gbcProfile.SetActive(false);
        clcProfile.SetActive(false);
        swcProfile.SetActive(false);
        PlayerPrefs.SetInt("ProfileEnabled", 0); PlayerPrefs.Save();
    }

    public void PlaySFX()
    {
        AudioManager.Instance.PlaySFX(clickSFX);
    }

    public void OpenScene()
    {
        SceneManager.LoadScene(identifyScene, LoadSceneMode.Additive);
    }
}
