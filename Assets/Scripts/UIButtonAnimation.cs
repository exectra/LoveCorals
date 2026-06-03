using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class UIButtonAnimation : MonoBehaviour,
    IPointerDownHandler,
    IPointerUpHandler
{
    [Header("Idle Float")]
    public bool enableIdleFloat = true;
    public float floatAmount = 8f;
    public float floatSpeed = 2f;

    [Header("Pop Animation")]
    public float pressedScale = 0.9f;
    public float popScale = 1.12f;

    public float pressSpeed = 12f;
    public float releaseSpeed = 10f;

    [Header("Ripple")]
    public Image rippleImage;
    public float rippleDuration = 0.4f;
    public float rippleMaxScale = 2f;

    private Vector3 startPos;
    private Vector3 originalScale;

    private Coroutine scaleRoutine;
    private Coroutine rippleRoutine;

    private void Start()
    {
        startPos = transform.localPosition;
        originalScale = transform.localScale;

        if (rippleImage != null)
        {
            rippleImage.gameObject.SetActive(false);
        }
    }

    private void Update()
    {
        if (enableIdleFloat)
        {
            float y = Mathf.Sin(Time.unscaledTime * floatSpeed) * floatAmount;

            transform.localPosition = startPos + new Vector3(0, y, 0);
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        AnimateScale(originalScale * pressedScale, pressSpeed);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (scaleRoutine != null)
            StopCoroutine(scaleRoutine);

        scaleRoutine = StartCoroutine(PopAnimation());

        if (rippleImage != null)
        {
            if (rippleRoutine != null)
                StopCoroutine(rippleRoutine);

            rippleRoutine = StartCoroutine(RippleEffect());
        }
    }

    private void AnimateScale(Vector3 target, float speed)
    {
        if (scaleRoutine != null)
            StopCoroutine(scaleRoutine);

        scaleRoutine = StartCoroutine(ScaleTo(target, speed));
    }

    private IEnumerator ScaleTo(Vector3 target, float speed)
    {
        while (Vector3.Distance(transform.localScale, target) > 0.01f)
        {
            transform.localScale = Vector3.Lerp(
                transform.localScale,
                target,
                speed * Time.unscaledDeltaTime
            );

            yield return null;
        }

        transform.localScale = target;
    }

    private IEnumerator PopAnimation()
    {
        // POP OUT
        while (Vector3.Distance(transform.localScale, originalScale * popScale) > 0.01f)
        {
            transform.localScale = Vector3.Lerp(
                transform.localScale,
                originalScale * popScale,
                releaseSpeed * Time.unscaledDeltaTime
            );

            yield return null;
        }

        // RETURN
        while (Vector3.Distance(transform.localScale, originalScale) > 0.01f)
        {
            transform.localScale = Vector3.Lerp(
                transform.localScale,
                originalScale,
                releaseSpeed * Time.unscaledDeltaTime
            );

            yield return null;
        }

        transform.localScale = originalScale;
    }

    private IEnumerator RippleEffect()
    {
        rippleImage.gameObject.SetActive(true);

        rippleImage.transform.localScale = Vector3.zero;

        Color startColor = rippleImage.color;
        float timer = 0f;

        while (timer < rippleDuration)
        {
            timer += Time.unscaledDeltaTime;

            float t = timer / rippleDuration;

            rippleImage.transform.localScale =
                Vector3.Lerp(Vector3.zero, Vector3.one * rippleMaxScale, t);

            Color c = startColor;
            c.a = Mathf.Lerp(startColor.a, 0f, t);

            rippleImage.color = c;

            yield return null;
        }

        rippleImage.gameObject.SetActive(false);

        Color reset = rippleImage.color;
        reset.a = 1f;
        rippleImage.color = reset;
    }
}