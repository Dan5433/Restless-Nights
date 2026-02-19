using System.Collections;
using UnityEngine;

public class IntroTransition : MonoBehaviour
{
    [SerializeField] ImageAlphaTransition transition;
    [SerializeField] TextAlphaTransition textTransition;
    [SerializeField] PlayerMovement playerMovement;

    private void Start()
    {
        playerMovement.Locked = true;

        StartCoroutine(StartTransition());
    }

    IEnumerator StartTransition()
    {
        StartCoroutine(textTransition.Play());
        yield return transition.FadeTransition();

        playerMovement.Locked = false;
    }
}
