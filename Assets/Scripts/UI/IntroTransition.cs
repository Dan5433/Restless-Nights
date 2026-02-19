using System.Collections;
using UnityEngine;

public class IntroTransition : MonoBehaviour
{
    [SerializeField] ImageAlphaTransition transition;
    [SerializeField] TextAlphaTransition timeTextTransition;
    [SerializeField] TextAlphaTransition nightTextTransition;
    [SerializeField] PlayerMovement playerMovement;

    private void Start()
    {
        playerMovement.Locked = true;

        StartCoroutine(StartTransition());
    }

    IEnumerator StartTransition()
    {
        StartCoroutine(timeTextTransition.Play());
        StartCoroutine(nightTextTransition.Play());
        yield return transition.FadeTransition();

        playerMovement.Locked = false;
    }
}
