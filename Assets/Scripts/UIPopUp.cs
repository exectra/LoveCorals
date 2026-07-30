using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIPopUp : MonoBehaviour
{
    [Header("Assign your popup Panel GameObject here")]
    public GameObject popupPanel;

    [Header("Fade Settings")]
    public float fadeDuration = 0.3f;

    [Header("Navigation")]
    [SerializeField] private GameObject pauseMenuPanel; 
    [SerializeField] private Button gotItButton;         
    [SerializeField] private AudioClip clickSFX;         

    private CanvasGroup canvasGroup;
    private Coroutine fadeRoutine;

    void Start()
    {
        if (popupPanel != null)
        {
            canvasGroup = popupPanel.GetComponent<CanvasGroup>();
            if (canvasGroup == null)
                canvasGroup = popupPanel.AddComponent<CanvasGroup>();

            canvasGroup.alpha = 0f;
            popupPanel.SetActive(false);
        }

        // ADD THIS — wire up the "Got it" button
        if (gotItButton != null)
        {
            gotItButton.onClick.AddListener(OnGotItPressed);
        }
    }

    public void ShowPopup()
    {
        if (popupPanel == null) return;
        popupPanel.SetActive(true);
        if (fadeRoutine != null) StopCoroutine(fadeRoutine);
        fadeRoutine = StartCoroutine(Fade(0f, 1f));
    }

    public void HidePopup()
    {
        if (popupPanel == null) return;
        if (fadeRoutine != null) StopCoroutine(fadeRoutine);
        fadeRoutine = StartCoroutine(Fade(1f, 0f, () => popupPanel.SetActive(false)));
    }

    // AGot it Button
    public void OnGotItPressed()
    {
        if (AudioManager.Instance != null && clickSFX != null)
        {
            AudioManager.Instance.PlaySFX(clickSFX);
        }

        HidePopup();

        if (pauseMenuPanel != null)
        {
            pauseMenuPanel.SetActive(true);
        }
    }

    private IEnumerator Fade(float from, float to, System.Action onComplete = null)
    {
        float elapsed = 0f;
        canvasGroup.alpha = from;
        canvasGroup.interactable = to > 0f;
        canvasGroup.blocksRaycasts = to > 0f;

        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(from, to, elapsed / fadeDuration);
            yield return null;
        }

        canvasGroup.alpha = to;
        onComplete?.Invoke();
    }
}
