using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Yarn.Unity;

public class DialogueStarter : MonoBehaviour
{
    public static class DialogueState
    {
        public static string NextNode;
    }

    public void BrainCoralSelected()
    {
        DialogueState.NextNode = "GroovedBrainCoralNode";

        SceneManager.LoadScene("GameScene");
    }
    public void SeaWhipCoralSelected()
    {
        DialogueState.NextNode = "SeaWhipCoralNode";

        SceneManager.LoadScene("GameScene");
    }
    public void CabbageCoralSelected()
    {
        DialogueState.NextNode = "CabbageCoralNode";

        SceneManager.LoadScene("GameScene");
    }
}