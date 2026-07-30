using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using CoralDating.Gifts;
using TMPro;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
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
    [SerializeField] public bool isBranch3;
    [SerializeField] public bool isGBBranch3;
    [SerializeField] private string lastGiftID;
    [SerializeField] private Image backgroundImage;

    [SerializeField] private string identifyScene = "(Game) IdentifyScene 2.0";
    public string LastGiftID => lastGiftID;
    [SerializeField] private GiftGivingManager giftGivingManager;

    public DialogueRunner dialogueRunner;
    private DialogueRunner registeredRunner;

    public TextMeshProUGUI coralPointsText;

    public static YarnCommandController Instance;
    private TaskCompletionSource<bool> identifyCompletionSource;


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

        yarnVariables = FindAnyObjectByType<YarnVariables>();
    }
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        var foundRunner = FindFirstObjectByType<DialogueRunner>();
        giftGivingManager = FindFirstObjectByType<GiftGivingManager>();

        var bgObj = GameObject.FindWithTag("Background");
        if (bgObj != null)
            backgroundImage = bgObj.GetComponent<Image>();

        var coralPtsObj = GameObject.FindWithTag("CoralPts");

        if (coralPtsObj != null)
        {
            coralPointsText = coralPtsObj.GetComponent<TextMeshProUGUI>();
            UpdateCoralPointsDisplay();
        }

        if (foundRunner != null)
        {
            dialogueRunner = foundRunner;
            dialogueRunner.VariableStorage = yarnVariables;

            if (registeredRunner != dialogueRunner)
            {
                GetScripts();
                registeredRunner = dialogueRunner;
            }

            if (!string.IsNullOrEmpty(DialogueState.NextNode))
            {
                dialogueRunner.startNode = DialogueState.NextNode;
                DialogueState.NextNode = "";
            }
        }

        if (DialogueState.NextBackground != null && backgroundImage != null)
        {
            backgroundImage.sprite = DialogueState.NextBackground;
            DialogueState.NextBackground = null;
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
            "isCLBranch3",
            CLBranch3
        );

        dialogueRunner.AddCommandHandler(
            "isGBBranch2",
            GBBranch
        );

        dialogueRunner.AddCommandHandler(
            "isGBBranch3",
            GBBranch3
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

    public void returnToHome()
    {
        SceneManager.LoadScene("MainMenu");
    }

    private async Task identifying()
    {
        identifyCompletionSource = new TaskCompletionSource<bool>();

        var loadOp = SceneManager.LoadSceneAsync(identifyScene, LoadSceneMode.Additive);
        await WaitForAsyncOperation(loadOp);

        // Dialogue pauses here until OnIdentifyComplete() is called
        await identifyCompletionSource.Task;
    }

    private Task WaitForAsyncOperation(AsyncOperation op)
    {
        var tcs = new TaskCompletionSource<bool>();
        op.completed += _ => tcs.TrySetResult(true);
        return tcs.Task;
    }

    public void identifyingComplete()
    {
        identifyCompletionSource?.TrySetResult(true);
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

    public void CLBranch3()
    {
        if (yarnVariables.TryGetValue("$CLBranch3", out bool isNextBranch))
        {
            isBranch3 = isNextBranch;

            Debug.Log("Updated proceeding to 3nd branch?: " + isBranch3);
        }
        else
        {
            Debug.LogError("Could not find $CLBranch3 in Yarn variables.");
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

    public void GBBranch3()
    {
        if (yarnVariables.TryGetValue("$GBBranch3", out bool isNextBranch))
        {
            isGBBranch3 = isNextBranch;

            Debug.Log("Updated proceeding to 3nd branch?: " + isGBBranch3);
        }
        else
        {
            Debug.LogError("Could not find $GBBranch3 in Yarn variables.");
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
