using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;
using TMPro;
using System.Collections;

public class IdentifySequence : MonoBehaviour
{
    [Header("Video")]
    public VideoPlayer videoPlayer;

    [Header("Rotate Intro")]
    public GameObject rotateOverlay;
    public RectTransform rotateIcon;
    public TMP_Text rotateText;
    public float rotateDuration = 0.3f;
    public float rotateHoldTime = 0.2f;

    [Header("Text")]
    public TMP_Text overlayText;

    [Header("Typing")]
    public float typingSpeed = 0.04f;
    public float phraseHoldTime = 0.8f;

    [Header("Reward")]
    public GameObject rewardPopup;

    [Header("Scene")]
    public string returnSceneName = "GameScene";

    private string[] phrases =
    {
        "Loading Data...",
        "Analysing Reef Pattern...",
        "Coral Correctly Identified!",
        "Affinity Points Increased by 10",
        "Party Hat Obtained"
    };

    private void Start()
    {
        Time.timeScale = 1f;

        if (rewardPopup != null)
            rewardPopup.SetActive(false);

        if (overlayText != null)
            overlayText.text = "";

        if (videoPlayer != null)
        {
            videoPlayer.isLooping = true;
            videoPlayer.Stop();
        }

        StartCoroutine(FullSequence());
    }

    private IEnumerator FullSequence()
    {
        if (videoPlayer != null)
            videoPlayer.Play();

        yield return StartCoroutine(PlayRotateIntro());

        yield return StartCoroutine(RunIdentificationSequence());

        yield return StartCoroutine(PlayRotateBackOutro());

        SceneManager.LoadScene(returnSceneName);
    }

    private IEnumerator PlayRotateIntro()
    {
        if (rotateIcon == null)
            yield break;

        rotateOverlay.SetActive(true);

        if (rotateText != null)
            rotateText.text = "Rotate your phone";

        rotateIcon.localRotation = Quaternion.Euler(0, 0, 0);

        float timer = 0f;

        while (timer < rotateDuration)
        {
            timer += Time.deltaTime;
            float t = timer / rotateDuration;

            float zRotation = Mathf.Lerp(0f, 180f, t);
            rotateIcon.localRotation = Quaternion.Euler(0, 0, zRotation);

            yield return null;
        }

        rotateIcon.localRotation = Quaternion.Euler(0, 0, 180f);

        yield return new WaitForSeconds(rotateHoldTime);

        rotateOverlay.SetActive(false);
    }

    private IEnumerator RunIdentificationSequence()
    {
        for (int i = 0; i < phrases.Length; i++)
        {
            if (phrases[i] == "Party Hat Obtained" && rewardPopup != null)
            {
                StartCoroutine(AnimateReward());
            }

            yield return StartCoroutine(TypePhrase(phrases[i]));
            yield return new WaitForSeconds(phraseHoldTime);
        }
    }

    private IEnumerator TypePhrase(string phrase)
    {
        overlayText.text = "";

        foreach (char c in phrase)
        {
            overlayText.text += c;
            yield return new WaitForSeconds(typingSpeed);
        }
    }

    private IEnumerator AnimateReward()
    {
        rewardPopup.SetActive(true);
        rewardPopup.transform.localScale = Vector3.zero;

        float timer = 0f;
        float duration = 0.25f;

        while (timer < duration)
        {
            timer += Time.deltaTime;
            float scale = Mathf.Lerp(0f, 1f, timer / duration);

            rewardPopup.transform.localScale = Vector3.one * scale;

            yield return null;
        }

        rewardPopup.transform.localScale = Vector3.one;
    }
    private IEnumerator PlayRotateBackOutro()
    {
        // HIDE IDENTIFICATION UI
        if (overlayText != null)
            overlayText.text = "";

        if (rewardPopup != null)
            rewardPopup.SetActive(false);

        if (rotateOverlay == null || rotateIcon == null)
            yield break;

        rotateOverlay.SetActive(true);

        if (rotateText != null)
            rotateText.text = "Return to Portrait Mode";

        rotateIcon.localRotation = Quaternion.Euler(0, 0, -180f);

        float timer = 0f;

        while (timer < rotateDuration)
        {
            timer += Time.deltaTime;

            float t = timer / rotateDuration;

            float zRotation = Mathf.Lerp(-180f, 0f, t);

            rotateIcon.localRotation = Quaternion.Euler(0, 0, zRotation);

            yield return null;
        }

        rotateIcon.localRotation = Quaternion.Euler(0, 0, 0f);

        yield return new WaitForSeconds(rotateHoldTime);

        rotateOverlay.SetActive(false);
    }
}