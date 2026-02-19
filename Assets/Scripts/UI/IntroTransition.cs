using System.Collections;
using UnityEngine;

public class IntroTransition : MonoBehaviour
{
    [SerializeField] ImageAlphaTransition transition;
    [SerializeField] PlayerMovement playerMovement;

    private void Start()
    {
        playerMovement.Locked = true;

        StartCoroutine(StartTransition());
    }

    IEnumerator StartTransition()
    {
        yield return transition.FadeTransition();

        playerMovement.Locked = false;
    }
}
