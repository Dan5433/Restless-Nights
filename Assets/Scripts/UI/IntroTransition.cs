using System.Collections;
using UnityEngine;

public class IntroTransition : MonoBehaviour
{
    [SerializeField] ImageAlphaTransition transition;
    [SerializeField] TextAlphaTransition timeTextTransition;
    [SerializeField] TextAlphaTransition nightTextTransition;
    [SerializeField] PlayerMovement playerMovement;
    [SerializeField] TextAlphaTransition dialogTextTransition;

    void Awake()
    {
        nightTextTransition.Text = GameManager.Instance.Night.name;
    }

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

        foreach (DialogManager.Message message in GameManager.Instance.Night.NightStartMessages)
            DialogManager.QueueMessage(message.text, message.fontStyle);
    }
}
