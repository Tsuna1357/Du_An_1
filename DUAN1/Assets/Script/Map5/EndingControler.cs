using UnityEngine;
using System.Collections;

public class EndingControler : MonoBehaviour
{
    public GameObject endingPanel;
    public GameObject ending1;
    public GameObject ending2;

    public CanvasGroup fadeGroup;

    public AudioSource audioSource;
    public AudioClip ending1Sound;
    public AudioClip ending2Sound;

    // Đang hiển thị Ending hay không
    public bool endingPlaying = false;

    private void Start()
    {
        endingPanel.SetActive(false);

        ending1.SetActive(false);
        ending2.SetActive(false);

        fadeGroup.alpha = 0;
    }

    public void ShowEnding1()
    {
        if (endingPlaying)
            return;

        endingPlaying = true;

        StartCoroutine(PlayEnding(
            ending1,
            ending1Sound
        ));
    }

    public void ShowEnding2()
    {
        if (endingPlaying)
            return;

        endingPlaying = true;

        StartCoroutine(PlayEnding(
            ending2,
            ending2Sound
        ));
    }

    private IEnumerator PlayEnding(
        GameObject ending,
        AudioClip sound)
    {
        endingPanel.SetActive(true);

        ending1.SetActive(false);
        ending2.SetActive(false);

        // Fade
        yield return StartCoroutine(FadeIn());

        // Hiện Ending
        ending.SetActive(true);

        // Âm thanh
        if (audioSource != null && sound != null)
        {
            audioSource.clip = sound;
            audioSource.Play();
        }

        yield return StartCoroutine(FadeOut());

        // Dừng gameplay
        Time.timeScale = 0f;
    }

    private IEnumerator FadeIn()
    {
        float time = 0f;
        float duration = 1f;

        while (time < duration)
        {
            time += Time.unscaledDeltaTime;

            fadeGroup.alpha = Mathf.Lerp(
                0f,
                1f,
                time / duration
            );

            yield return null;
        }

        fadeGroup.alpha = 1f;
    }

    private IEnumerator FadeOut()
    {
        float time = 0f;
        float duration = 1f;

        while (time < duration)
        {
            time += Time.unscaledDeltaTime;

            fadeGroup.alpha = Mathf.Lerp(
                1f,
                0f,
                time / duration
            );

            yield return null;
        }

        fadeGroup.alpha = 0f;
    }

    public void CloseEnding()
    {
        Time.timeScale = 1f;

        endingPlaying = false;

        if (audioSource != null)
        {
            audioSource.Stop();
        }

        endingPanel.SetActive(false);

        ending1.SetActive(false);
        ending2.SetActive(false);
    }
}