using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FadeIn : MonoBehaviour
{
    public Image blackScreen; // This refers to the Image component of the Panel
    [Tooltip("Duration of the fade effect in seconds")]
    public float fadeSpeed = 1f;
    [Tooltip("Delay before the fade effect starts in seconds")]
    public float delayBeforeFade = 2f; // Delay in seconds

    void Start()
    {
        blackScreen.canvasRenderer.SetAlpha(1.0f);
        StartCoroutine(DelayedFadeOut());
    }

    IEnumerator DelayedFadeOut()
    {
        yield return new WaitForSeconds(delayBeforeFade);
        blackScreen.CrossFadeAlpha(0, fadeSpeed, false);
    }
}
