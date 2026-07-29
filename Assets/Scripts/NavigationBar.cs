using UnityEngine;
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

    [SerializeField] private Tab[] tabs;
    [SerializeField] private int defaultTab = -1;
    [SerializeField] private AudioClip clickSFX;

    private bool initialize = false;
    private int currentTab = -1;

    private void Start()
    {
        for (int i = 0; i < tabs.Length; i++)
        {
            int index = i;
            tabs[i].button.onClick.AddListener(() => OpenTab(index));

            if (tabs[i].panel != null) tabs[i].panel.SetActive(false);
            tabs[i].icon.sprite = tabs[i].normalSprite;
            tabs[i].iconTransform.localScale = Vector3.one;
        }

        if (defaultTab >= 0 && defaultTab < tabs.Length)
        {
            OpenTab(defaultTab);
        }

        initialize = true;
    }

    public void OpenTab(int index)
    {
        if (currentTab == index)
            return;

        // CHANGED: added AudioManager.Instance != null check
        if (initialize && clickSFX != null && AudioManager.Instance != null)
        {
            AudioManager.Instance.PlaySFX(clickSFX);
        }

        for (int i = 0; i < tabs.Length; i++)
        {
            bool selected = i == index;
            if (tabs[i].panel != null) tabs[i].panel.SetActive(selected);
            tabs[i].icon.sprite = selected ? tabs[i].selectedSprite : tabs[i].normalSprite;
            tabs[i].iconTransform.localScale = selected ? Vector3.one * 1.2f : Vector3.one;
        }

        currentTab = index;
    }
}