using System.Collections;
using UnityEngine;

public class IntroTransition : MonoBehaviour
{
    [SerializeField] ImageAlphaTransition transition;
    [SerializeField] TextAlphaTransition timeTextTransition;
    [SerializeField] TextAlphaTransition nightTextTransition;
    [SerializeField] PlayerMovement playerMovement;
    [SerializeField] TextAlphaTransition dialogTextTransition;

    private void Start()
    {
        playerMovement.Locked = true;

        StartCoroutine(StartTransition());
    }

    IEnumerator StartTransition()
    {
        StartCoroutine(timeTextTransition.Play());
        StartCoroutine(nightTextTransition.Play());
        yield return transition.Play();

        playerMovement.Locked = false;

        DialogManager.QueueMessage("You feel a foreign presence."); //TODO: play customized per night wake up message
        DialogManager.QueueMessage("You begin to panic.");
    }
}
