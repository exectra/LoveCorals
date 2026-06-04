using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [Header("Sources")]
    [SerializeField] private AudioSource audioSourceBGM;
    [SerializeField] private AudioSource audioSourceSFX;

    [Header("Clips")]
    [SerializeField] private AudioClip menuBGM;
    [SerializeField] private AudioClip gameBGM;

    private AudioClip currentBGM;
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

    // Start is called before the first frame update
    void Start()
    {
        audioSourceBGM = GameObject.Find("BGM").GetComponent<AudioSource>();
        audioSourceSFX = GameObject.Find("SFX").GetComponent<AudioSource>();
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        StartCoroutine(HandleBGM(scene.name));
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    public void PlaySFX(AudioClip clip)
    {
        audioSourceSFX.PlayOneShot(clip);
    }

    public float PlaySFXAndGetLength(AudioClip clip)
    {
        audioSourceSFX.PlayOneShot(clip);
        return clip.length;
    }

    private AudioClip GetBGM(string sceneName)
    {
        switch (sceneName)
        {
            case "MainMenu":
                return menuBGM;

            case "GameScene":
                return gameBGM;

            case "CoralIdentify":
                return gameBGM;

            default:
                return null;
        }
    }

    private IEnumerator HandleBGM(string sceneName)
    {
        AudioClip nextClip = GetBGM(sceneName);

        if (nextClip == null)
            yield break;

        // If same BGM is already playing, do nothing
        if (audioSourceBGM.isPlaying && audioSourceBGM.clip == nextClip)
        {
            currentBGM = nextClip;
            yield break;
        }

        // fade out only if something is playing
        if (audioSourceBGM.isPlaying)
            yield return FadeOutBGM(1f);

        // switch clip
        audioSourceBGM.clip = nextClip;
        audioSourceBGM.loop = true;
        audioSourceBGM.Play();

        currentBGM = nextClip;

        // fade in new music
        yield return FadeInBGM(nextClip, 0.15f);
    }

    public IEnumerator FadeOutBGM(float duration)
    {
        if (audioSourceBGM == null || !audioSourceBGM.isPlaying)
            yield break;

        float startVolume = audioSourceBGM.volume;

        for (float t = 0; t < duration; t += Time.unscaledDeltaTime)
        {
            audioSourceBGM.volume = Mathf.Lerp(startVolume, 0f, t / duration);
            yield return null;
        }

        audioSourceBGM.volume = 0f;
        audioSourceBGM.Stop();
    }

    public IEnumerator FadeInBGM(AudioClip clip, float duration, float targetVolume = 0.15f)
    {
        if (audioSourceBGM == null || clip == null)
            yield break;

        audioSourceBGM.clip = clip;
        audioSourceBGM.loop = true;
        audioSourceBGM.volume = 0f;
        audioSourceBGM.Play();

        for (float t = 0; t < duration; t += Time.unscaledDeltaTime)
        {
            audioSourceBGM.volume = Mathf.Lerp(0f, targetVolume, t / duration);
            yield return null;
        }

        audioSourceBGM.volume = targetVolume;
    }
}
