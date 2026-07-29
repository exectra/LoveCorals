using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public static SceneLoader Instance;

    [SerializeField] private AudioClip clickSFX;
    [SerializeField] private AudioManager AM;
    [SerializeField] string identifyScene = "(Main Menu) IdentifyScene 2.0";

    public string previousScene;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        AM = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        previousScene = SceneManager.GetActiveScene().name;
    }
    public static SceneLoader GetInstance()
    {
        if (Instance == null)
        {
            Instance = FindFirstObjectByType<SceneLoader>();
        }
        return Instance;
    }

    public void LoadGame()
    {
        AM.PlaySFX(clickSFX);
        SceneManager.LoadScene("GameScene");
    }

    public void LoadMenu()
    {
        AM.PlaySFX(clickSFX);
        SceneManager.LoadScene("MainMenu");
    }
    public void OpenIdentifyScene()
    {
        AM.PlaySFX(clickSFX);
        //DontDestroyOnLoad(FindObjectOfType<Canvas>());
        SceneManager.LoadScene(identifyScene, LoadSceneMode.Additive);
    }

}