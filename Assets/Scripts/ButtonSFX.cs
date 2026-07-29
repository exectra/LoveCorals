using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonSFX : MonoBehaviour
{
    [SerializeField] private AudioClip clickSFX;
    [SerializeField] private AudioManager AM;
    // Start is called before the first frame update
    void Awake()
    {
        AM = GameObject.Find("AudioManager").GetComponent<AudioManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void PlaySFX()
    {
        AudioManager.Instance.PlaySFX(clickSFX);
    }

}
