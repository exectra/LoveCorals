using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CharacterAffinity : MonoBehaviour
{
    public string characterName;
    public TextMeshProUGUI affinityText;

    [Range(0, 100)]
    public int affinity = 0;

    public int affinityLevel = 1;

    //TESTING
    private void Update()
    {
        // Test gift (+10 affinity)
        if (Input.GetKeyDown(KeyCode.G))
        {
            AddAffinity(10);
            Debug.Log("Gift given to " + characterName);
        }

        // Test negative action (-5 affinity)
        if (Input.GetKeyDown(KeyCode.F))
        {
            AddAffinity(-5);
            Debug.Log("Bad interaction with " + characterName);
        }
    }

    public void AddAffinity(int amount)
    {
        affinity += amount;
        affinity = Mathf.Clamp(affinity, 0, 100);

        UpdateAffinityLevel();
    }

    private void UpdateAffinityLevel()
    {
        if (affinity >= 80)
            affinityLevel = 5;
        else if (affinity >= 60)
            affinityLevel = 4;
        else if (affinity >= 40)
            affinityLevel = 3;
        else if (affinity >= 20)
            affinityLevel = 2;
        else
            affinityLevel = 1;

        affinityText.text = "Affinity level: " + affinityLevel.ToString();
        Debug.Log(characterName + " Affinity Level: " + affinityLevel);
    }
}
