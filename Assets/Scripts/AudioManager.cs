using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [Header("Sources")]
    [SerializeField] private AudioSource audioSourceBGM;
    [SerializeField] private AudioSource audioSourceSFX;

    [SerializeField] private AudioClip PauseSFX;

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

    public void PlayButtonClick(AudioClip sfxClip)
    {
        audioSourceSFX.PlayOneShot(sfxClip);
    }

    public void PlaySFX(AudioClip clip)
    {
        audioSourceSFX.PlayOneShot(clip);
    }

    public void PlayPauseSFX()
    {
        audioSourceSFX.PlayOneShot(PauseSFX);
    }

    public float PlaySFXAndGetLength(AudioClip clip)
    {
        audioSourceSFX.PlayOneShot(clip);
        return clip.length;
    }
}
