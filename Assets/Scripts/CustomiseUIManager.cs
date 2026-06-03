using UnityEngine;
using TMPro;
using System.Collections;

public class CustomisationUIManager : MonoBehaviour
{
    [Header("Bottom Equipment Panel")]
    public RectTransform equipmentPanel;
    [SerializeField] Vector2 hiddenPos = new Vector2(1000, 320);
    [SerializeField] Vector2 shownPos = new Vector2(0, 320);
    public float slideSpeed = 900f;

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
    private string currentCategory = "";
    private int currentOpenIndex = -1;

    private void Start()
    {
        ShowSlotGroup(0);
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
        // If clicking same category while panel is open -> close it
        if (panelOpen && currentOpenIndex == slotIndex)
        {
            ClosePanel();
            currentOpenIndex = -1;
            return;
        }

        currentOpenIndex = slotIndex;

        currentCategory = categoryName;

        if (categoryText != null)
            categoryText.text = categoryName;

        ShowBoxesAndLines();
        ShowSlotGroup(slotIndex);

        if (!panelOpen)
        {
            panelOpen = true;
            SlideTo(shownPos);
        }
    }

    private void ShowSlotGroup(int index)
    {
        if (headgearSlots != null) headgearSlots.SetActive(index == 0);
        if (bodySlots != null) bodySlots.SetActive(index == 1);
        if (handsSlots != null) handsSlots.SetActive(index == 2);
        if (feetSlots != null) feetSlots.SetActive(index == 3);
    }

    public void ClosePanel()
    {
        panelOpen = false;
        currentOpenIndex = -1;

        SlideTo(hiddenPos);
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

        // If boxes are now hidden, close gear panel too
        if (!boxesVisible && panelOpen)
            ClosePanel();
    }

    public void HideBoxesAndLines()
    {
        boxesVisible = false;

        if (boxesAndLinesParent != null)
            boxesAndLinesParent.SetActive(false);

        if (panelOpen)
            ClosePanel();
    }

    public void ShowBoxesAndLines()
    {
        boxesVisible = true;

        if (boxesAndLinesParent != null)
            boxesAndLinesParent.SetActive(true);
    }

    private void SlideTo(Vector2 target)
    {
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