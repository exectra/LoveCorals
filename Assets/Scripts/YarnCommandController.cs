using CoralDating.Gifts;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Yarn.Unity;
using Yarn.Unity.Samples;

public class YarnCommandController : MonoBehaviour
{
    [SerializeField] private YarnVariables yarnVariables;
    [SerializeField] public float coralPoints;
    [SerializeField] public string currentSpeaker;

    [SerializeField] private GiftGivingManager giftGivingManager;

    public DialogueRunner dialogueRunner;

    //public GameObject coralPointsObj;
    public TextMeshProUGUI coralPointsText;


    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("YarnCommandController Awake");
        dialogueRunner = FindObjectOfType<DialogueRunner>();
        //coralPointsNum = coralPointsObj.GetComponent<TextMeshProUGUI>();

        dialogueRunner.AddCommandHandler(
            "update_coral_points_display",
            UpdateCoralPointsDisplay
        );

        dialogueRunner.AddCommandHandler(
            "Update_CurrentSpeaker",
            CurrentSpeaker
        );

        dialogueRunner.AddCommandHandler<string>(
            "GiveGift",
            GiveGiftCommand
        );

        if (giftGivingManager == null)
        {
            giftGivingManager = FindObjectOfType<GiftGivingManager>();
        }




    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //[YarnCommand("update_CoralPoints_display")]
    public void UpdateCoralPointsDisplay()
    {
        if (yarnVariables.TryGetValue("$SWCoralPoints", out float points))
        {
            coralPoints = points;

            // Update the TextMeshPro text

            coralPointsText.text = coralPoints.ToString();
            if (coralPointsText != null)
            {
                coralPointsText.text = coralPoints.ToString("0");
            }

            Debug.Log("Updated coralPoints: " + coralPoints);
        }
        else
        {
            Debug.LogError("Could not find $SWCoralPoints in Yarn variables.");
        }
    }

    public void CurrentSpeaker()
    {
        if (yarnVariables.TryGetValue("$Speaker", out string speaker))
        {
            currentSpeaker = speaker;

            Debug.Log("Updated speaker: " + currentSpeaker);
        }
        else
        {
            Debug.LogError("Could not find $Speaker in Yarn variables.");
        }
    }

    private void GiveGiftCommand(string coralID)
    {
        Debug.Log($"Giving gift to {coralID}");

        giftGivingManager.OpenGiftMenu(coralID);
    }
}
