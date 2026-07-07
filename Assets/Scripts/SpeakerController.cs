using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Speaker
{
    public string name;
    public Sprite image;
}

public class SpeakerController : MonoBehaviour
{
    [SerializeField] Image currentSpeaker;
    [SerializeField] YarnCommandController commandController;
    [SerializeField]
    private List<Speaker> speakers;

    private Dictionary<string, Speaker> speakerImagesDic = new();
    private void Awake()
    {
        foreach (Speaker speaker in speakers)
        {
            speakerImagesDic[speaker.name] = speaker;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        commandController = FindAnyObjectByType<YarnCommandController> ();
    }

    // Update is called once per frame
    void Update()
    {
        if(speakerImagesDic.TryGetValue(commandController.currentSpeaker, out Speaker portrait))
        {
            currentSpeaker.sprite = portrait.image; 
        }
        else
        {
            //Debug.Log("not replacing");
        }
    }
}
