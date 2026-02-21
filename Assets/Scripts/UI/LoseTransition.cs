using EditorAttributes;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]
public class LoseTransition : MonoBehaviour
{
    [SerializeField] PlayerMovement playerMovement;
    [SerializeField] GameObject loseScreen;
    AudioSource[] audioSources;
    AudioSource loseAudioSource;

    private void Awake()
    {
        audioSources = (AudioSource[])FindObjectsOfType(typeof(AudioSource), true);

        loseAudioSource = GetComponent<AudioSource>();
    }

    [Button("Test Lose", 36)]
    public void Lose()
    {
        playerMovement.Locked = true;

        foreach (AudioSource source in audioSources)
            source.mute = true;

        loseScreen.SetActive(true);

        loseAudioSource.mute = false;
        StartCoroutine(PlayTransition());


    }

    IEnumerator PlayTransition()
    {
        loseAudioSource.Play();

        yield return new WaitWhile(() => loseAudioSource.isPlaying);

        loseScreen.GetComponent<Image>().color = Color.red;
    }
}
