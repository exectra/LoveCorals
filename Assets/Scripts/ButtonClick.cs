using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonClick : MonoBehaviour
{

    [Header("Audio")]
    [SerializeField] private AudioClip clickSFX;
    [SerializeField] private AudioManager AM;

    // Start is called before the first frame update
    void Start()
    {
        var found = GameObject.Find("AudioManager");
        if (found != null)
            AM = found.GetComponent<AudioManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayButtonSFX(AudioClip clip)
    {
        if (AM != null && clip != null) AM.PlaySFX(clip);
    }
}
