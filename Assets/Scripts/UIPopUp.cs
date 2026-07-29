using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIPopUp : MonoBehaviour
{
    [Header("Assign your popup Panel GameObject here")]
    public GameObject popupPanel;

    [Header("Fade Settings")]
    public float fadeDuration = 0.3f;

    private CanvasGroup canvasGroup;
    private Coroutine fadeRoutine;

    void Start()
    {
        if (popupPanel != null)
        {
            canvasGroup = popupPanel.GetComponent<CanvasGroup>();

            // Auto-add CanvasGroup if missing
            if (canvasGroup == null)
                canvasGroup = popupPanel.AddComponent<CanvasGroup>();

            canvasGroup.alpha = 0f;
            popupPanel.SetActive(false);
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

    private IEnumerator Fade(float from, float to, System.Action onComplete = null)
    {
        float elapsed = 0f;
        canvasGroup.alpha = from;

        // Optional: allow clicking through while invisible
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
