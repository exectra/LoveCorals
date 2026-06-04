using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public GameObject popupPanel;

    private bool isOpen;

    [SerializeField] private AudioClip PauseSFX;
    [SerializeField] private AudioClip UnpauseSFX;
    [SerializeField] private AudioManager AM;

    private void Start()
    {
        AM = GameObject.Find("AudioManager").GetComponent<AudioManager>();

        if (popupPanel != null)
            popupPanel.SetActive(false);

        isOpen = false;
    }
        
    public void Toggle()
    {
        if (isOpen) ClosePopup();
        else OpenPopup();
    }

    public void OpenPopup()
    {
        if (popupPanel == null) return;

        isOpen = true;
        popupPanel.SetActive(true);
        AM.PlaySFX(PauseSFX);

        // optional safety: close inventory if open
        var inv = FindObjectOfType<InventoryDrawer>();
        if (inv != null) inv.CloseInstant();

        Time.timeScale = 0f;
    }

    public void ClosePopup()
    {
        if (popupPanel == null) return;

        isOpen = false;
        popupPanel.SetActive(false);
        AM.PlaySFX(UnpauseSFX);

        Time.timeScale = 1f;
    }

    public void ResumeGame()
    {
        ClosePopup();
    }

    public void QuitToMenu()
    {
        Time.timeScale = 1f;

        if (SceneLoader.Instance != null)
            SceneLoader.Instance.LoadMenu();
    }
}