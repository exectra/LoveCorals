using System;
using System.Collections;
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
    public RectTransform equipmentPanel;
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
    private void Start()
    {
        // Make sure all gear slots are always visible
        ShowAllGearSlots();

        // Start with panel hidden
        /*if (equipmentPanel != null)
            equipmentPanel.anchoredPosition = hiddenPos;*/

        panelOpen = false;
        currentOpenIndex = -1;

        //add interxn on 
        AddInteractionOnOpen();
    }

    public void OpenHeadgear()
    {
        OpenCategory("Crown (Headgear)", 0);

    }

    public void OpenBody()
    {
        OpenCategory("Gills (Body)", 1);

    }

    public void OpenHands()
    {
        OpenCategory("Flippers (Hands)", 2);

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

/*    private void SlideTo(Vector2 target)
    {
        if (equipmentPanel == null)
            return;

        if (slideRoutine != null)
            StopCoroutine(slideRoutine);

        slideRoutine = StartCoroutine(Slide(target));
    }

    private IEnumerator Slide(Vector2 target)
    {
        while (Vector2.Distance(equipmentPanel.anchoredPosition, target) > 0.5f)
        {
            equipmentPanel.anchoredPosition = Vector2.MoveTowards(
                equipmentPanel.anchoredPosition,
                target,
                slideSpeed * Time.unscaledDeltaTime
            );

            yield return null;
        }

        equipmentPanel.anchoredPosition = target;
    }*/

    //Van added this section onwards
    //Slot interactions + Updating corresponding item slots + Showing item on Sharkie

    //Slot interaction
    private void AddInteractionOnOpen(/*int currentIndex*/)
    {
        int index = 0;
        foreach (Transform child in itemSlots.transform)
        {
            Debug.Log(child.name);
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
            int currentCapturedIndex = index;

            // Pass the structural database pointer directly through the anonymous click function
            btn.onClick.AddListener(() => OnSlotClicked(currentCapturedIndex));

            // Increment the tracking index counter for the next slot child
            index++;
        }
    }

    private void OnSlotClicked(int databaseSlotID)
    {
        Debug.Log($"Player selected UI Slot Index: {databaseSlotID}");

        // Example database check logic:
        // ItemData selectedItem = ItemDatabase.GetItemFromCategory(currentCategory, databaseSlotID);
        // CharacterManager.Equip(selectedItem);

        GameObject[] arr = GameObject.FindGameObjectsWithTag("Database");

        if (databaseSlotID >= arr.Length) 
        {
            Debug.Log("Out of Bounds");
            hatDisplay.GetComponent<Image>().sprite = null; hatDisplay.GetComponent<Image>().color = new Color(255f, 255f, 255f, 0f);
        }       
        else
        {
            Sprite image = arr[databaseSlotID].GetComponent<Image>().sprite;
            if (hatDisplay.GetComponent<Image>().sprite == null)
            {
                hatDisplay.GetComponent<Image>().sprite = image;
                hatDisplay.GetComponent<Image>().color = Color.white;
            }
            else
            {
                if (hatDisplay.GetComponent<Image>().sprite.name == image.name) { hatDisplay.GetComponent<Image>().sprite = null; hatDisplay.GetComponent<Image>().color = new Color(255f, 255f, 255f, 0f); }
                else if(hatDisplay.GetComponent<Image>().sprite.name != image.name && image != null) { hatDisplay.GetComponent<Image>().sprite = image; hatDisplay.GetComponent<Image>().color = new Color(255f, 255f, 255f, 0f); }
                else { hatDisplay.GetComponent<Image>().sprite = null; hatDisplay.GetComponent<Image>().color = new Color(255f, 255f, 255f, 0f); }
            }
        }
    }
}