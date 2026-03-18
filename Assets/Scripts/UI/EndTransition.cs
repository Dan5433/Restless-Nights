using EditorAttributes;
using System.Collections;
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

        endScreen.SetActive(true);

        audioSource.mute = false;
    }

    IEnumerator PlayLoseTransition()
    {
        audioSource.Play();

        yield return new WaitWhile(() => audioSource.isPlaying);

        endScreen.GetComponent<Image>().color = loseColor;
        audioSource.clip = loseAudio;
        audioSource.Play();
    }

    IEnumerator PlayWinTransition()
    {
        audioSource.Play();

        yield return new WaitWhile(() => audioSource.isPlaying);

        audioSource.clip = winAudio;
        audioSource.Play();

        Image endScreenImage = endScreen.GetComponent<Image>();
        while (audioSource.isPlaying)
        {
            float playPosition = audioSource.time / audioSource.clip.length;
            endScreenImage.color = new(playPosition, playPosition, playPosition);
            yield return null;
        }
    }

}
