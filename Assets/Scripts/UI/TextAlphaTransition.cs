using EditorAttributes;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TextAlphaTransition : MonoBehaviour
{
    [SerializeField] TMP_Text text;
    [SerializeField] float easeTime = 1f;
    [SerializeField] float holdTime = 0.25f;

    public float EaseTime => easeTime;
    public float HoldTime => holdTime;

#if UNITY_EDITOR
    [Button("Play", 36)]
    void PlayTransition()
    {
        StartCoroutine(Play());
    }
#endif

    public IEnumerator Play()
    {
        float time = 0;
        Color color = text.color;

        while (time < easeTime)
        {
            color = text.color;
            color.a += 1 / (easeTime / Time.deltaTime);
            text.color = color;

            time += Time.deltaTime;
            yield return null;
        }

        color.a = 1;
        text.color = color;

        yield return new WaitForSeconds(holdTime);

        time = 0;
        while (time < easeTime)
        {
            color = text.color;
            color.a -= 1 / (easeTime / Time.deltaTime);
            text.color = color;

            time += Time.deltaTime;
            yield return null;
        }

        color.a = 0;
        text.color = color;
    }

}
