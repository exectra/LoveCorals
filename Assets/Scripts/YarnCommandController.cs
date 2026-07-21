using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using CoralDating.Gifts;
using TMPro;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using Yarn.Unity;
using Yarn.Unity.Samples;
using static DialogueStarter;


public class YarnCommandController : MonoBehaviour
{
    [SerializeField] private YarnVariables yarnVariables;
    [SerializeField] public float coralPoints;
    [SerializeField] public string currentSpeaker;
    [SerializeField] public bool isBranch2;
    [SerializeField] public bool isGBBranch2;
    [SerializeField] private string lastGiftID;
    public string LastGiftID => lastGiftID;
    [SerializeField] private GiftGivingManager giftGivingManager;

    public DialogueRunner dialogueRunner;

    //public GameObject coralPointsObj;
    public TextMeshProUGUI coralPointsText;

    public static YarnCommandController Instance;

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("YarnCommandController Awake");
        //dialogueRunner = FindObjectOfType<DialogueRunner>();
        yarnVariables = FindAnyObjectByType<YarnVariables>();
        ////coralPointsNum = coralPointsObj.GetComponent<TextMeshProUGUI>();

        //dialogueRunner.AddCommandHandler(
        //    "update_coral_points_display",
        //    UpdateCoralPointsDisplay
        //);

        //dialogueRunner.AddCommandHandler(
        //    "Update_CurrentSpeaker",
        //    CurrentSpeaker
        //);

        //dialogueRunner.AddCommandHandler<string>(
        //    "GiveGift",
        //    GiveGiftCommand
        //);

        //dialogueRunner.AddCommandHandler(
        //    "returnToHome",
        //    returnToHome
        //);

        //dialogueRunner.AddCommandHandler(
        //    "isCLBranch2",
        //    CLBranch
        //);

        //if (giftGivingManager == null)
        //{
        //    giftGivingManager = FindObjectOfType<GiftGivingManager>();
        //}

        //if (!string.IsNullOrEmpty(DialogueState.NextNode))
        //{
        //    dialogueRunner.startNode = DialogueState.NextNode;
        //    Debug.Log(DialogueState.NextNode);
        //    //dialogueRunner.StartDialogue(DialogueState.NextNode);
        //    DialogueState.NextNode = ""; // Clear it after use

        //}
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        dialogueRunner = FindFirstObjectByType<DialogueRunner>();
        giftGivingManager = FindFirstObjectByType<GiftGivingManager>();
        coralPointsText = GameObject.FindWithTag("CoralPts").GetComponent<TextMeshProUGUI>();

        Debug.Log($"Scene Loaded: {scene.name}");

        if (dialogueRunner != null)
        {
            // Make the DialogueRunner use the singleton's YarnVariables
            dialogueRunner.VariableStorage = yarnVariables;
            GetScripts();

            if (!string.IsNullOrEmpty(DialogueState.NextNode))
            {
                dialogueRunner.startNode = DialogueState.NextNode;
                DialogueState.NextNode = "";
            }
        }
    }

    private void GetScripts()
    {
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

        dialogueRunner.AddCommandHandler(
            "returnToHome",
            returnToHome
        );

        dialogueRunner.AddCommandHandler(
            "identifyCorals",
            identifying
        );

        dialogueRunner.AddCommandHandler(
            "isCLBranch2",
            CLBranch
        );

        dialogueRunner.AddCommandHandler(
            "isGBBranch2",
            GBBranch
        );
        dialogueRunner.AddFunction(
            "LastGift",
            () => lastGiftID
        );
    }

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

    //[YarnCommand("returnMainMenu")]
    public void returnToHome()
    {
        SceneManager.LoadScene("MainMenu");
    }
    public void identifying()
    {
        SceneManager.LoadScene("CoralIdentify", LoadSceneMode.Additive);
    }

    //get the bool if the player has already finish the first branch for Cabbage coral
    public void CLBranch()
    {
        if (yarnVariables.TryGetValue("$CLBranch", out bool isNextBranch))
        {
            isBranch2 = isNextBranch;

            Debug.Log("Updated proceeding to 2nd branch?: " + isBranch2);
        }
        else
        {
            Debug.LogError("Could not find $CLBranch in Yarn variables.");
        }
    }

    //get the bool if the player has already finish the first branch for Grooved Brain coral
    public void GBBranch()
    {
        if (yarnVariables.TryGetValue("$GBBranch", out bool isNextBranch))
        {
            isGBBranch2 = isNextBranch;

            Debug.Log("Updated proceeding to 2nd branch?: " + isGBBranch2);
        }
        else
        {
            Debug.LogError("Could not find $GBBranch in Yarn variables.");
        }
    }

    private async Task GiveGiftCommand(string coralID)
    {
        GiftData selectedGift = await giftGivingManager.OpenGiftMenu(coralID);

        if (selectedGift == null)
            return;

        lastGiftID = selectedGift.giftID;

        Debug.Log($"Player gave {selectedGift.displayName} to {coralID}");
    }
}
