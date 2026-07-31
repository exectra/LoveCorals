using UnityEngine;
using UnityEngine.SceneManagement;

public class GameUIManager : MonoBehaviour
{
    public static GameUIManager Instance;

    public InventoryDrawer inventoryDrawer;
    public PauseMenu pauseMenu;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }
    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "GameScene")
        {
            pauseMenu = FindObjectOfType<PauseMenu>();
            inventoryDrawer = FindObjectOfType<InventoryDrawer>();

            if (pauseMenu != null && pauseMenu.gameObject.activeSelf)
            {
                pauseMenu.gameObject.SetActive(false);
            }
            else if (pauseMenu == null)
            {
                Debug.LogWarning("PauseMenu not found in GameScene.");
            }
        }
    }
}