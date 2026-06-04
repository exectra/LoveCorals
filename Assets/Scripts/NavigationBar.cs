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
    [SerializeField] private int defaultTab = 0;

    [SerializeField] private AudioClip clickSFX;
    private bool initialize = false;

    private int currentTab = -1;

    private void Start()
    {
        for (int i = 0; i < tabs.Length; i++)
        {
            int index = i;

            tabs[i].button.onClick.AddListener(() => OpenTab(index));
        }

        OpenTab(defaultTab);
        initialize = true;
    }
    public void OpenTab(int index)
    {
        if (currentTab == index)
            return;

        if (initialize && clickSFX != null)
        {
            AudioManager.Instance.PlaySFX(clickSFX);
        }

        for (int i = 0; i < tabs.Length; i++)
        {
            bool selected = i == index;

            tabs[i].panel.SetActive(selected);

            tabs[i].icon.sprite = selected
                ? tabs[i].selectedSprite
                : tabs[i].normalSprite;

            tabs[i].iconTransform.localScale = selected
                ? Vector3.one * 1.2f
                : Vector3.one;
        }

        currentTab = index;
    }
}