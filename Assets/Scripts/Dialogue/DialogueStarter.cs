using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Yarn.Unity;

public class DialogueStarter : MonoBehaviour
{
    [SerializeField] YarnCommandController commandController;

    void Start()
    {
        commandController = FindAnyObjectByType<YarnCommandController>();
    }

    public static class DialogueState
    {
        public static string NextNode;
    }

    public void BrainCoralSelected()
    {
        if (commandController.isGBBranch3)
        {
            DialogueState.NextNode = "GroovedBrainCoralNode3";
        }
        else if (commandController.isGBBranch2)
        {
            DialogueState.NextNode = "GroovedBrainCoralNode2";
        }
        else
        {
            DialogueState.NextNode = "GroovedBrainCoralNode";
        }


        SceneManager.LoadScene("GameScene");
    }
    public void SeaWhipCoralSelected()
    {
        DialogueState.NextNode = "SeaWhipCoralNode";

        SceneManager.LoadScene("GameScene");
    }
    public void CabbageCoralSelected()
    {
        if (commandController.isBranch3)
        {
            Debug.Log("branch 3");
            DialogueState.NextNode = "CabbageCoralNode3";
        }
        else if (commandController.isBranch2)
        {
            Debug.Log("branch 2");
            DialogueState.NextNode = "CabbageCoralNode2";
        }
        else
        {
            DialogueState.NextNode = "CabbageCoralNode";
        }

        SceneManager.LoadScene("GameScene");
    }
}