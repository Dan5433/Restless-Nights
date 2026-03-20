using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogManager : Singleton<DialogManager>
{
    [SerializeField] GameObject dialog;
    TMP_Text text;
    TextAlphaTransition transition;
    Queue<Message> messageQueue = new();

    protected override void Awake()
    {
        base.Awake();

        text = dialog.GetComponent<TMP_Text>();
        transition = dialog.GetComponent<TextAlphaTransition>();
    }

    void Start()
    {
        StartCoroutine(TryDisplayMessage());
    }

    public static void QueueMessage(string message, FontStyles fontStyle = FontStyles.Normal)
    {
        if (!IsInstanceValid())
            return;

        Instance.messageQueue.Enqueue(new()
        {
            text = message,
            fontStyle = fontStyle,
        });
    }

    IEnumerator TryDisplayMessage()
    {
        while (true)
        {
            if (messageQueue.Count == 0)
            {
                yield return null;
                continue;
            }

            Message message = messageQueue.Dequeue();
            text.text = message.text;
            text.fontStyle |= message.fontStyle;

            yield return transition.Play();

            text.fontStyle ^= message.fontStyle;
        }
    }

    struct Message
    {
        public string text;
        public FontStyles fontStyle;
    }
}
