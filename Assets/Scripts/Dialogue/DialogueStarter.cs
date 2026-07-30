using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Yarn.Unity;

public class DialogueStarter : MonoBehaviour
{
    [SerializeField] YarnCommandController commandController;
    [System.Serializable]
    public class RouteBackground
    {
        public string routeID;
        public Sprite background;
    }

    [SerializeField] List<RouteBackground> backgroundList;
    private Dictionary<string, Sprite> backgroundLookup;


    void Start()
    {
        commandController = FindAnyObjectByType<YarnCommandController>();
        backgroundLookup = new Dictionary<string, Sprite>();
        foreach (var rb in backgroundList)
        {
            if (!backgroundLookup.ContainsKey(rb.routeID))
                backgroundLookup.Add(rb.routeID, rb.background);
        }
    }

    public static class DialogueState
    {
        public static string NextNode;
        public static Sprite NextBackground; //carries the sprite across the scene load
    }

    private Sprite GetBackground(string routeID)
    {
        if (backgroundLookup.TryGetValue(routeID, out Sprite sprite))
            return sprite;

        Debug.LogWarning($"No background found for route: {routeID}");
        return null;
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

        DialogueState.NextBackground = GetBackground("GroovedBrainCoral");
        SceneManager.LoadScene("GameScene");
    }
    public void SeaWhipCoralSelected()
    {
        DialogueState.NextNode = "SeaWhipCoralNode";
        DialogueState.NextBackground = GetBackground("SeaWhipCoral");

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

        DialogueState.NextBackground = GetBackground("CabbageCoral");
        SceneManager.LoadScene("GameScene");
    }
}