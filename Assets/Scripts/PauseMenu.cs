using UnityEngine;

public class PausePopup : MonoBehaviour
{
    public GameObject popupPanel;

    private bool isOpen = false;

    private void Start()
    {
        popupPanel.SetActive(false);
    }

    private void Update()
    {
        if (!GameUIManager.Instance.IsGameActive()) return;

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Toggle();
        }
    }

    public void Toggle()
    {
        if (isOpen) ClosePopup();
        else OpenPopup();
    }

    public void OpenPopup()
    {
        isOpen = true;
        popupPanel.SetActive(true);

        Time.timeScale = 0f; // pause game
    }

    public void ClosePopup()
    {
        isOpen = false;
        popupPanel.SetActive(false);

        Time.timeScale = 1f;
    }

    public void ResumeGame()
    {
        ClosePopup();
    }

    public void QuitToMenu()
    {
        Time.timeScale = 1f;
        GameUIManager.Instance.ReturnToMenu();
    }
}