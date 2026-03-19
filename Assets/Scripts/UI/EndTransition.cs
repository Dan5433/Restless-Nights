using EditorAttributes;
using Extensions;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]
public class EndTransition : MonoBehaviour
{
    [SerializeField] PlayerMovement playerMovement;
    [SerializeField] GameObject endScreen;
    [SerializeField] Color[] loseColors = { Color.red, Color.red, Color.red, Color.red };
    [SerializeField] AudioClip loseAudio, winAudio;
    [SerializeField] int loseAudioRepeats;
    [SerializeField] float loseAudioRepeatRate, returnDelay;
    AudioSource[] audioSources;
    AudioSource audioSource;

    private void Awake()
    {
        audioSources = (AudioSource[])FindObjectsOfType(typeof(AudioSource), true);

        audioSource = GetComponent<AudioSource>();
    }

    [Button("Test Lose", 36)]
    public void Lose()
    {
        SetupNightEnd();
        StartCoroutine(PlayLoseTransition());
    }

    [Button("Test Win", 36)]
    public void Win()
    {
        SetupNightEnd();
        StartCoroutine(PlayWinTransition());
    }

    void SetupNightEnd()
    {
        playerMovement.Locked = true;

        foreach (AudioSource source in audioSources)
            source.mute = true;

        audioSource.mute = false;
        audioSource.Play();

        endScreen.SetActive(true);
    }

    IEnumerator PlayLoseTransition()
    {
        yield return new WaitWhile(() => audioSource.isPlaying);

        var endScreenImage = endScreen.GetComponent<Image>();
        endScreenImage.color = loseColors[0];
        var text = endScreen.GetComponentInChildren<TMP_Text>();
        text.color = Color.black;
        text.text = string.Empty;

        string[] words = { "They ", "found ", "you." };
        for (int i = 0; i < loseAudioRepeats; i++)
        {
            yield return new WaitForSeconds(loseAudioRepeatRate);
            endScreenImage.color = loseColors[i + 1];
            text.text += words[i];
            audioSource.PlayOneShotWithRandomPitch(loseAudio, 0.8f, 1.2f);
        }

        yield return new WaitForSeconds(returnDelay);
        ReturnToMainMenu();
    }

    IEnumerator PlayWinTransition()
    {
        yield return new WaitWhile(() => audioSource.isPlaying);

        var text = endScreen.GetComponentInChildren<TMP_Text>();
        text.color = Color.black;
        text.text = "7:00 AM";

        audioSource.clip = winAudio;
        audioSource.Play();

        var endScreenImage = endScreen.GetComponent<Image>();
        Color textColor = text.color;
        float time = 0;
        while (audioSource.isPlaying)
        {
            float playPosition = time / winAudio.length;

            endScreenImage.color = new(playPosition, playPosition, playPosition);

            textColor.a = playPosition;
            text.color = textColor;

            time += Time.deltaTime;
            yield return null;
        }

        yield return new WaitForSeconds(returnDelay);
        ReturnToMainMenu();
    }

    void ReturnToMainMenu()
    {
        Debug.Log("return to menu");
    }
}
