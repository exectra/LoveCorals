using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CustomisationUIManager : MonoBehaviour
{
    [Header("Bottom Equipment Panel")]
    public GameObject invSlots;
    [SerializeField] Vector2 hiddenPos = new Vector2(1000, 320);
    [SerializeField] Vector2 shownPos = new Vector2(0, 320);
    private float slideSpeed = 1000f;

    [Header("Category Text")]
    public TMP_Text categoryText;

    [Header("Boxes + Lines Parent")]
    public GameObject boxesAndLinesParent;

    [Header("Slot Groups")]
    public GameObject itemSlots;

    private Coroutine slideRoutine;
    private bool panelOpen = false;
    private bool boxesVisible = true;
    private int currentOpenIndex = -1;

    [Header("DisplaySlots")]
    public GameObject hatDisplay;
    public GameObject handDisplay;
    public GameObject bodyDisplay;
    public GameObject feetDisplay;

    [Header("ImageRef")]
    public GameObject imageRef;

    [Header("Hat")]
    public Sprite[] hatArr;

    [Header("Hand")]
    public Sprite[] handArr;

    private List<GameObject> instantList;

    private void Start()
    {
        instantList = new List<GameObject>();
        // Make sure all gear slots are always visible
        ShowAllGearSlots();

        panelOpen = false;
        currentOpenIndex = -1;

        //add interxn on 
        //AddInteractionOnOpen();
        AddHats();
    }

    public void OpenHeadgear()
    {
        OpenCategory("Crown (Headgear)", 0);
        AddHats();
    }

    public void OpenBody()
    {
        OpenCategory("Gills (Body)", 1);

    }

    public void OpenHands()
    {
        OpenCategory("Flippers (Hands)", 2);
        AddHands();
    }

    public void OpenFeet()
    {
        OpenCategory("Tailwear (Feet)", 3);

    }

    private void OpenCategory(string categoryName, int slotIndex)
    {
        // If clicking the same category while panel is open, close the panel
        if (panelOpen && currentOpenIndex == slotIndex)
        {
            ClosePanel();
            return;
        }

        currentOpenIndex = slotIndex;

        /*if (categoryText != null)
            categoryText.text = categoryName;*/

        ShowAllGearSlots();

        panelOpen = true;
        //SlideTo(shownPos);

    }

    private void ShowAllGearSlots()
    {
        if (itemSlots != null) itemSlots.SetActive(true);
    }

    public void ClosePanel()
    {
        panelOpen = false;
        currentOpenIndex = -1;

        ClearSelectedButton();

        //SlideTo(hiddenPos);
    }
    private void ClearSelectedButton()
    {
        if (EventSystem.current != null)
            EventSystem.current.SetSelectedGameObject(null);
    }

    public void TogglePanel()
    {
        if (panelOpen)
            ClosePanel();
        else
            OpenHeadgear();
    }

    public void ToggleBoxesAndLines()
    {
        boxesVisible = !boxesVisible;

        if (boxesAndLinesParent != null)
            boxesAndLinesParent.SetActive(boxesVisible);

        // If boxes are hidden, close gear panel too
        if (!boxesVisible && panelOpen)
            ClosePanel();
    }

    //Van added this section onwards
    //Slot interactions + Updating corresponding item slots + Showing item on Sharkie

    //Slot interaction

    //this function DOESNT EXIST

    private void AddHats()
    {
        DestroyChild();

        int i = 0;
        foreach (Transform child in itemSlots.transform)
        {
            if (i < hatArr.Length)
            {
                GameObject img = Instantiate(imageRef, child);
                instantList.Add(img);
                img.GetComponentInChildren<Image>().sprite = hatArr[i];

                if (!child.gameObject.TryGetComponent<Button>(out Button btn))
                {
                    btn = child.gameObject.AddComponent<Button>();
                }


                ColorBlock colorBlock = btn.colors;
                colorBlock.highlightedColor = Color.cyan;

                btn.colors = colorBlock;

                // CRITICAL FIX: Clear old click hooks before assigning the current loop's tracking data
                btn.onClick.RemoveAllListeners();

                // Use a local copy variable for the lambda expression capture trap
                int currentCapturedIndex = i;
                string type = "Hats";
                // Pass the structural database pointer directly through the anonymous click function
                btn.onClick.AddListener(() => OnSlotClicked(currentCapturedIndex, type));

                // Increment the tracking index counter for the next slot child
                i++;
            }
            else
            {
                break;
            }

        }
    }

    private void AddHands()
    {
        DestroyChild();

        int i = 0;
        foreach (Transform child in itemSlots.transform)
        {
            if (i < handArr.Length)
            {
                GameObject img = Instantiate(imageRef, child);
                instantList.Add(img);
                img.GetComponentInChildren<Image>().sprite = handArr[i];

                if (!child.gameObject.TryGetComponent<Button>(out Button btn))
                {
                    btn = child.gameObject.AddComponent<Button>();
                }


                ColorBlock colorBlock = btn.colors;
                colorBlock.highlightedColor = Color.cyan;

                btn.colors = colorBlock;

                // CRITICAL FIX: Clear old click hooks before assigning the current loop's tracking data
                btn.onClick.RemoveAllListeners();

                // Use a local copy variable for the lambda expression capture trap
                int currentCapturedIndex = i;
                string type = "Hands";
                // Pass the structural database pointer directly through the anonymous click function
                btn.onClick.AddListener(() => OnSlotClicked(currentCapturedIndex, type));

                // Increment the tracking index counter for the next slot child
                i++;
            }
            else { break; }
        }
    }

    private void DestroyChild()
    {
        if (instantList != null)
        {
            foreach (GameObject i in instantList)
            {
                Destroy(i);
            }
            instantList.Clear();
        }
    }

    private void KillButtons()
    {
        foreach (Transform child in itemSlots.transform)
        {
            if (child.gameObject.TryGetComponent<Button>(out Button button))
            {
                Destroy(button);
            }
        }
    }

    private void OnSlotClicked(int i, string type)
    {
        if (type == "Hands")
        {

            if (handDisplay.GetComponent<Image>().sprite != null)
            {
                Sprite prevSprite = handDisplay.GetComponent<Image>().sprite;
                if (prevSprite != handArr[i])
                {
                    handDisplay.GetComponent<Image>().sprite = handArr[i];
                    handDisplay.GetComponent<Image>().color = Color.white;
                }
                else
                {
                    handDisplay.GetComponent<Image>().sprite = null;
                    handDisplay.GetComponent <Image>().color = Color.clear;
                }

            }
            else
            {
                handDisplay.GetComponent<Image>().sprite = handArr[i];
                handDisplay.GetComponent<Image>().color = Color.white;
            }
            Debug.Log(handDisplay.GetComponent<Image>().sprite.name);
        }

        if (type == "Hats")
        {

            if (hatDisplay.GetComponent<Image>().sprite != null)
            {
                Sprite prevSprite = hatDisplay.GetComponent<Image>().sprite;
                if (prevSprite != hatArr[i])
                {
                    hatDisplay.GetComponent<Image>().sprite = hatArr[i];
                    hatDisplay.GetComponent<Image>().color = Color.white;
                }
                else
                {
                    hatDisplay.GetComponent<Image>().sprite = null;
                    hatDisplay.GetComponent<Image>().color = Color.clear;
                }
            }
            else
            {
                hatDisplay.GetComponent<Image>().sprite = hatArr[i];
                hatDisplay.GetComponent<Image>().color = Color.white;
            }
            Debug.Log(hatDisplay.GetComponent<Image>().sprite.name);
        }
    }
}