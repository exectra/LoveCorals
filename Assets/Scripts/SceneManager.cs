using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public static SceneLoader Instance;

    [SerializeField] private AudioClip clickSFX;
    [SerializeField] private AudioManager AM;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        AM = GameObject.Find("AudioManager").GetComponent<AudioManager>();
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
        SceneManager.LoadScene("CoralIdentify");
    }


    public void LoadScene(string sceneName, AudioClip clip)
    {
        StartCoroutine(LoadAfterSFX(sceneName, clip));
    }

    private IEnumerator LoadAfterSFX(string sceneName, AudioClip clip)
    {
        float delay = AudioManager.Instance.PlaySFXAndGetLength(clip);

        yield return new WaitForSeconds(delay);

        SceneManager.LoadScene(sceneName);
    }
}