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
        sceneManager = GameObject.Find("GameManager").GetComponent<SceneManager>(); ;
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

    public void OpenScene()
    {
        SceneManager.LoadScene(identifyScene, LoadSceneMode.Additive);
    }
}
