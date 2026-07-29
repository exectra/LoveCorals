using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class NavigationBar : MonoBehaviour
{
    [System.Serializable]
    public class Tab
    {
        public Button button;
        public Image icon;
        public RectTransform iconTransform;
        public Sprite normalSprite;
        public Sprite selectedSprite;
        public GameObject panel;
    }

    [Header("Tabs")]
    [SerializeField] private Tab[] tabs;
    [SerializeField] private int defaultTab = 0;

    [Header("Audio")]
    [SerializeField] private AudioClip clickSFX;

    [Header("Warning Popup")]
    [SerializeField] private GameObject warningPanel;

    private bool initialize = false;
    private int currentTab = -1;
    private int pendingTab = -1;

    // Controls whether the warning should appear
    private bool warningEnabled = true;

    private void Start()
    {
        // Setup all tab buttons
        for (int i = 0; i < tabs.Length; i++)
        {
            int index = i;
            tabs[i].button.onClick.AddListener(() => ShowWarning(index));

            if (tabs[i].panel != null)
                tabs[i].panel.SetActive(false);

            if (tabs[i].icon != null)
                tabs[i].icon.sprite = tabs[i].normalSprite;

            if (tabs[i].iconTransform != null)
                tabs[i].iconTransform.localScale = Vector3.one;
        }

        // Hide popup if one exists
        if (warningPanel != null)
            warningPanel.SetActive(false);

        // If there is no warning panel assigned in this scene,
        // disable the warning automatically.
        if (warningPanel == null)
            warningEnabled = false;

        // Open default tab
        if (defaultTab >= 0 && defaultTab < tabs.Length)
        {
            OpenTab(defaultTab);
        }

        initialize = true;
    }

    private void ShowWarning(int index)
    {
        // Already on this tab
        if (currentTab == index)
            return;

        // Warning disabled? Just switch tabs.
        if (!warningEnabled)
        {
            OpenTab(index);
            return;
        }

        // Show popup
        pendingTab = index;

        if (warningPanel != null)
            warningPanel.SetActive(true);
    }

    // Called by the Cancel button
    public void CancelWarning()
    {
        if (warningPanel != null)
            warningPanel.SetActive(false);

        pendingTab = -1;
    }

    // Called by the Continue/Proceed button
    public void ConfirmWarning()
    {
        if (warningPanel != null)
            warningPanel.SetActive(false);

        warningEnabled = false;

        // Close the additive Identify scene
        SceneLoader.GetInstance().CloseIdentifyScene();

        if (pendingTab >= 0)
        {
            OpenTab(pendingTab);
            pendingTab = -1;
        }
    }

    // Call this when starting a NEW identification game.
    public void EnableWarning()
    {
        warningEnabled = true;
    }

    public void OpenTab(int index)
    {
        if (currentTab == index)
            return;

        // Play click sound (not on initial load)
        if (initialize && clickSFX != null && AudioManager.Instance != null)
        {
            AudioManager.Instance.PlaySFX(clickSFX);
        }

        for (int i = 0; i < tabs.Length; i++)
        {
            bool selected = (i == index);

            if (tabs[i].panel != null)
                tabs[i].panel.SetActive(selected);

            if (tabs[i].icon != null)
                tabs[i].icon.sprite = selected ? tabs[i].selectedSprite : tabs[i].normalSprite;

            if (tabs[i].iconTransform != null)
                tabs[i].iconTransform.localScale = selected ? Vector3.one * 1.2f : Vector3.one;
        }

        currentTab = index;
    }
}