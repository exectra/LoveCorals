using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.EventSystems;

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
    public GameObject headgearSlots;
    public GameObject bodySlots;
    public GameObject handsSlots;
    public GameObject feetSlots;

    private Coroutine slideRoutine;
    private bool panelOpen = false;
    private bool boxesVisible = true;
    private int currentOpenIndex = -1;

    private void Start()
    {
        // Make sure all gear slots are always visible
        ShowAllGearSlots();

        // Start with panel hidden
        if (equipmentPanel != null)
            equipmentPanel.anchoredPosition = hiddenPos;

        panelOpen = false;
        currentOpenIndex = -1;
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

        if (categoryText != null)
            categoryText.text = categoryName;

        ShowAllGearSlots();

        panelOpen = true;
        SlideTo(shownPos);
    }

    private void ShowAllGearSlots()
    {
        if (headgearSlots != null) headgearSlots.SetActive(true);
        if (bodySlots != null) bodySlots.SetActive(true);
        if (handsSlots != null) handsSlots.SetActive(true);
        if (feetSlots != null) feetSlots.SetActive(true);
    }

    public void ClosePanel()
    {
        panelOpen = false;
        currentOpenIndex = -1;

        ClearSelectedButton();

        SlideTo(hiddenPos);
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

    private void SlideTo(Vector2 target)
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
    }
}