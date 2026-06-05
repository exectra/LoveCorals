using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;
using Yarn.Unity.Samples;
using TMPro;

public class YarnCommandController : MonoBehaviour
{
    [SerializeField] private YarnVariables yarnVariables;
    [SerializeField] public float SWaffinity;

    public DialogueRunner dialogueRunner;

    //public GameObject affinityObj;
    public TextMeshProUGUI affinityNum;


    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("YarnCommandController Awake");
        dialogueRunner = FindObjectOfType<DialogueRunner>();
        //affinityNum = affinityObj.GetComponent<TextMeshProUGUI>();

        dialogueRunner.AddCommandHandler(
            "update_affinity_display",
            UpdateAffinityDisplay
        );
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //[YarnCommand("update_affinity_display")]
    public void UpdateAffinityDisplay()
    {
        if (yarnVariables.TryGetValue("$SWaffinity", out float affinity))
        {
            SWaffinity = affinity;

            // Update the TextMeshPro text
            affinityNum.text = SWaffinity.ToString();

            Debug.Log("Updated affinity: " + SWaffinity);
        }
        else
        {
            Debug.LogError("Could not find $SWaffinity in Yarn variables.");
        }
    }
}
