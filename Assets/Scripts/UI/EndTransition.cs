using EditorAttributes;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]
public class EndTransition : MonoBehaviour
{
    [SerializeField] PlayerMovement playerMovement;
    [SerializeField] GameObject endScreen;
    [SerializeField] Color loseColor = Color.red;
    [SerializeField] AudioClip loseAudio, winAudio;
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
        NightEnd();
        StartCoroutine(PlayLoseTransition());
    }

    [Button("Test Win", 36)]
    public void Win()
    {
        NightEnd();
        StartCoroutine(PlayWinTransition());
    }

    void NightEnd()
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

        audioSource.clip = loseAudio;
        audioSource.Play();

        endScreen.GetComponent<Image>().color = loseColor;
        var text = endScreen.GetComponentInChildren<TMP_Text>();
        text.color = Color.black;
        text.text = "They found you.";
    }

    IEnumerator PlayWinTransition()
    {
        yield return new WaitWhile(() => audioSource.isPlaying);

        var text = endScreen.GetComponentInChildren<TMP_Text>();
        text.color = Color.black;
        text.text = "7:00 AM";

        audioSource.clip = winAudio;
        audioSource.Play();

        Image endScreenImage = endScreen.GetComponent<Image>();
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
    }

}
