using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InventoryDrawer : MonoBehaviour
{
    //public RectTransform panel;
    //[Header("Tabs")]
    //public GameObject itemsTab;
    //public GameObject affinityTab;
    //[Header("Tab Buttons")]
    //public UnityEngine.UI.Image itemsButton;
    //public UnityEngine.UI.Image affinityButton;
    //public Color selectedColor = Color.white;
    //public Color unselectedColor = Color.gray;
    //[Header("Animation")]
    //public float slideSpeed = 12f;

    [Header("Menu Buttons")]
    public RectTransform homeButton;
    public RectTransform inventoryButton;
    //public RectTransform customisationButton;
    public RectTransform settingsButton;
    public float buttonSpacing = 70f;      // vertical gap between buttons when open
    public float buttonStagger = 0.05f;    // delay between each button's animation
    public float buttonSlideSpeed = 12f;

    [Header("Audio")]
    [SerializeField] private AudioClip tabSFX;
    [SerializeField] private AudioClip OpenSFX;
    [SerializeField] private AudioClip CloseSFX;
    [SerializeField] private AudioManager AM;
    [SerializeField] Vector2 hiddenPos;
    [SerializeField] Vector2 shownPos;
    private bool isOpen;
    private Coroutine animRoutine;
    private bool initialized = false;

    private RectTransform[] menuButtons;
    private Vector2[] buttonHiddenPos;
    private Vector2[] buttonShownPos;
    private Coroutine[] buttonRoutines;

    [Header("Settings Panel")]
    public GameObject settingsPanel;

    [Header("Inventory Panel")]
    public GameObject dialogueSystem;       // drag "Dialogue System" GameObject here
    public RectTransform inventoryPanel;    // drag your InventoryUI panel's RectTransform here
    public float inventorySlideSpeed = 12f;
    public float inventoryOffscreenY = -2000f;  // how far below screen it starts hidden
    [SerializeField] private GameObject itemDetailsPanel;

    [SerializeField] private float raisedOffset = 180f;

    private Vector2 inventoryRaisedPos;
    private Vector2 inventoryShownPos;
    private Vector2 inventoryHiddenPos;
    private Coroutine inventoryRoutine;
    private bool inventoryOpen = false;

    private void Start()
    {
        // shownPos = new Vector2(-400, 0);
        // hiddenPos = new Vector2(0, 0);
        // panel.anchoredPosition = hiddenPos;   // panel was null, caused NullReferenceException

        menuButtons = new RectTransform[] { homeButton, inventoryButton, settingsButton };
        InitButtons();

        if (inventoryPanel != null)
        {
            inventoryShownPos = inventoryPanel.anchoredPosition;                          // wherever you placed it in the Editor
            inventoryRaisedPos = inventoryShownPos + Vector2.up * raisedOffset;
            inventoryHiddenPos = new Vector2(inventoryShownPos.x, inventoryOffscreenY);    // below the screen
            inventoryPanel.anchoredPosition = inventoryHiddenPos;
            inventoryPanel.gameObject.SetActive(false);
        }

        var found = GameObject.Find("AudioManager");
        if (found != null)
            AM = found.GetComponent<AudioManager>();

        // ShowTab(0);
        isOpen = false;
        initialized = true;
    }

    private void InitButtons()
    {
        int n = menuButtons.Length;
        buttonHiddenPos = new Vector2[n];
        buttonShownPos = new Vector2[n];
        buttonRoutines = new Coroutine[n];

        for (int i = 0; i < n; i++)
        {
            buttonShownPos[i] = menuButtons[i].anchoredPosition;   // ← whatever you placed it at in the Editor
            buttonHiddenPos[i] = Vector2.zero;                     // collapses to hamburger button's position
            menuButtons[i].anchoredPosition = buttonHiddenPos[i];
            menuButtons[i].gameObject.SetActive(false);
        }
    }

    public void Toggle()
    {
        if (isOpen) Close();
        else Open();
    }

    public void Open()
    {
        isOpen = true;
        // StartAnim(shownPos);

        PlaySfxSafe(OpenSFX);

        for (int i = 0; i < menuButtons.Length; i++)
        {
            menuButtons[i].gameObject.SetActive(true);
            if (buttonRoutines[i] != null) StopCoroutine(buttonRoutines[i]);
            buttonRoutines[i] = StartCoroutine(SlideButton(i, buttonShownPos[i], buttonStagger * i));
        }
    }

    public void Close()
    {
        isOpen = false;
        // StartAnim(hiddenPos);
        PlaySfxSafe(CloseSFX);

        for (int i = 0; i < menuButtons.Length; i++)
        {
            if (buttonRoutines[i] != null) StopCoroutine(buttonRoutines[i]);
            buttonRoutines[i] = StartCoroutine(SlideButtonOut(i, buttonHiddenPos[i], buttonStagger * i));
        }
    }

    public void CloseInstant()
    {
        isOpen = false;
        if (animRoutine != null)
            StopCoroutine(animRoutine);
        // panel.anchoredPosition = hiddenPos;

        for (int i = 0; i < menuButtons.Length; i++)
        {
            if (buttonRoutines[i] != null) StopCoroutine(buttonRoutines[i]);
            menuButtons[i].anchoredPosition = buttonHiddenPos[i];
            menuButtons[i].gameObject.SetActive(false);
        }
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void ToggleSettingsPanel()
    {
        if (settingsPanel != null)
            settingsPanel.SetActive(!settingsPanel.activeSelf);
    }

    public void ToggleInventory()
    {
        if (inventoryOpen) CloseInventory();
        else OpenInventory();
    }

    public void OpenInventory()
    {
        Debug.Log("OpenInventory Called");
        if (inventoryPanel == null) return;

        inventoryOpen = true;
        inventoryPanel.gameObject.SetActive(true);

        if (itemDetailsPanel != null)
            itemDetailsPanel.SetActive(false);

        //if (dialogueSystem != null)
        //    dialogueSystem.SetActive(false);   // hide dialogue while inventory is open

        if (inventoryRoutine != null) StopCoroutine(inventoryRoutine);
        inventoryRoutine = StartCoroutine(SlideInventory(inventoryShownPos));
    }

    public void GoToCustomization()
    {
        PlayerPrefs.SetInt("GoToCustom", 1);
        PlayerPrefs.Save();
        SceneManager.LoadScene("MainMenu");
    }

    public void CloseInventory()
    {
        Debug.Log("CloseInventory Called");
        Debug.Log("inventoryOpen before = " + inventoryOpen);
        if (inventoryPanel == null) return;

        inventoryOpen = false;

        Debug.Log("inventoryOpen after = " + inventoryOpen);
        if (itemDetailsPanel != null)
            itemDetailsPanel.SetActive(false);

        //if (dialogueSystem != null)
        //    dialogueSystem.SetActive(true);    // bring dialogue back when inventory closes

        Debug.Log("Starting SlideInventoryOut");
        if (inventoryRoutine != null) StopCoroutine(inventoryRoutine);
        inventoryRoutine = StartCoroutine(SlideInventoryOut(inventoryHiddenPos));
    }

    private IEnumerator SlideInventory(Vector2 target)
    {
        while (Vector2.Distance(inventoryPanel.anchoredPosition, target) > 0.5f)
        {
            inventoryPanel.anchoredPosition = Vector2.MoveTowards(inventoryPanel.anchoredPosition, target, inventorySlideSpeed * 800f * Time.unscaledDeltaTime);
            yield return null;
        }
        inventoryPanel.anchoredPosition = target;
    }

    private IEnumerator SlideInventoryOut(Vector2 target)
    {

        Debug.Log("SlideInventoryOut started");
        while (Vector2.Distance(inventoryPanel.anchoredPosition, target) > 0.5f)
        {
            inventoryPanel.anchoredPosition = Vector2.MoveTowards(inventoryPanel.anchoredPosition, target, inventorySlideSpeed * 800f * Time.unscaledDeltaTime);
            yield return null;
        }

        Debug.Log("Inventory hidden");
        inventoryPanel.anchoredPosition = target;
        inventoryPanel.gameObject.SetActive(false);
    }

    private void PlaySfxSafe(AudioClip clip)
    {
        if (AM != null && clip != null) AM.PlaySFX(clip);
    }

    //private void StartAnim(Vector2 target)
    //{
    //    if (animRoutine != null)
    //        StopCoroutine(animRoutine);
    //    animRoutine = StartCoroutine(Slide(target));
    //}

    //private IEnumerator Slide(Vector2 target)
    //{
    //    while (Vector2.Distance(panel.anchoredPosition, target) > 0.5f)
    //    {
    //        panel.anchoredPosition = Vector2.MoveTowards(
    //            panel.anchoredPosition,
    //            target,
    //            slideSpeed * 800f * Time.unscaledDeltaTime
    //        );
    //        yield return null;
    //    }
    //    panel.anchoredPosition = target;
    //}

    private IEnumerator SlideButton(int i, Vector2 target, float delay)
    {
        if (delay > 0) yield return new WaitForSecondsRealtime(delay);
        RectTransform t = menuButtons[i];
        while (Vector2.Distance(t.anchoredPosition, target) > 0.5f)
        {
            t.anchoredPosition = Vector2.MoveTowards(t.anchoredPosition, target, buttonSlideSpeed * 800f * Time.unscaledDeltaTime);
            yield return null;
        }
        t.anchoredPosition = target;
    }

    private IEnumerator SlideButtonOut(int i, Vector2 target, float delay)
    {
        if (delay > 0) yield return new WaitForSecondsRealtime(delay);
        RectTransform t = menuButtons[i];
        while (Vector2.Distance(t.anchoredPosition, target) > 0.5f)
        {
            t.anchoredPosition = Vector2.MoveTowards(t.anchoredPosition, target, buttonSlideSpeed * 800f * Time.unscaledDeltaTime);
            yield return null;
        }
        t.anchoredPosition = target;
        menuButtons[i].gameObject.SetActive(false);
    }
    public void RaiseInventory()
    {
        Debug.Log("RaiseInventory Called");
        if (inventoryRoutine != null)
            StopCoroutine(inventoryRoutine);

        inventoryRoutine =
            StartCoroutine(SlideInventory(inventoryRaisedPos));
    }

    public void LowerInventory()
    {
        Debug.Log("LowerInventory Called");
        if (inventoryRoutine != null)
            StopCoroutine(inventoryRoutine);

        inventoryRoutine =
            StartCoroutine(SlideInventory(inventoryShownPos));
    }

    // ---------------- TAB SYSTEM ----------------
    /*
    public void ShowTab(int index)
    {
        if (itemsTab == null || affinityTab == null) return;
        if(initialized)
            AM.PlaySFX(tabSFX);
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
    */
}

