using UnityEngine;

public class GameUIManager : MonoBehaviour
{
    public static GameUIManager Instance;

    [Header("Main UI")]
    public GameObject navbarUI;
    public GameObject inGameHUD;

    [Header("Systems")]
    public InventoryDrawer inventoryDrawer;
    public PausePopup pausePopup;

    private bool isGameActive = false;

    private void Awake()
    {
        Instance = this;
    }

    public void StartGame()
    {
        isGameActive = true;

        navbarUI.SetActive(false);
        inGameHUD.SetActive(true);

        inventoryDrawer.CloseInstant();
        pausePopup.ClosePopup();
    }

    public void ReturnToMenu()
    {
        isGameActive = false;

        navbarUI.SetActive(true);
        inGameHUD.SetActive(false);

        inventoryDrawer.CloseInstant();
        pausePopup.ClosePopup();
    }

    public bool IsGameActive()
    {
        return isGameActive;
    }
}