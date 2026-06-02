using UnityEngine;
using System.Collections;

public class InventoryDrawer : MonoBehaviour
{
    public RectTransform panel;

    [Header("Tabs")]
    public GameObject itemsTab;
    public GameObject affinityTab;

    [Header("Tab Buttons")]
    public UnityEngine.UI.Image itemsButton;
    public UnityEngine.UI.Image affinityButton;

    public Color selectedColor = Color.white;
    public Color unselectedColor = Color.gray;

    [Header("Animation")]
    public float slideSpeed = 12f;

    [SerializeField] Vector2 hiddenPos;
    [SerializeField] Vector2 shownPos;

    private bool isOpen;
    private Coroutine animRoutine;

    private void Start()
    {

        panel.anchoredPosition = hiddenPos;
        ShowTab(0);
        isOpen = false;
    }

    public void Toggle()
    {
        if (isOpen) Close();
        else Open();
    }

    public void Open()
    {
        isOpen = true;
        StartAnim(shownPos);
    }

    public void Close()
    {
        isOpen = false;
        StartAnim(hiddenPos);
    }

    public void CloseInstant()
    {
        isOpen = false;

        if (animRoutine != null)
            StopCoroutine(animRoutine);

        panel.anchoredPosition = hiddenPos;
    }

    private void StartAnim(Vector2 target)
    {
        if (animRoutine != null)
            StopCoroutine(animRoutine);

        animRoutine = StartCoroutine(Slide(target));
    }

    private IEnumerator Slide(Vector2 target)
    {
        while (Vector2.Distance(panel.anchoredPosition, target) > 0.5f)
        {
            panel.anchoredPosition = Vector2.MoveTowards(
                panel.anchoredPosition,
                target,
                slideSpeed * 800f * Time.unscaledDeltaTime
            );

            yield return null;
        }

        panel.anchoredPosition = target;
    }

    // ---------------- TAB SYSTEM ----------------

    public void ShowTab(int index)
    {
        if (itemsTab == null || affinityTab == null) return;

        itemsTab.SetActive(index == 0);
        affinityTab.SetActive(index == 1);

        if (itemsButton != null && affinityButton != null)
        {
            itemsButton.color = (index == 0) ? selectedColor : unselectedColor;
            affinityButton.color = (index == 1) ? selectedColor : unselectedColor;
        }
    }

    public void OpenItems() => ShowTab(0);
    public void OpenAffinity() => ShowTab(1);
}