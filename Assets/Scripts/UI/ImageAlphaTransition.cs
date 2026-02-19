using EditorAttributes;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ImageAlphaTransition : MonoBehaviour
{
    [SerializeField] Image image;
    [SerializeField] float easeTime = 1f;
    [SerializeField] float holdTime = 0.25f;

    public float EaseTime => easeTime;
    public float HoldTime => holdTime;

#if UNITY_EDITOR
    [Button("Play", 36)]
    void PlayTransition()
    {
        StartCoroutine(FadeTransition());
    }
#endif

    public IEnumerator FadeTransition()
    {
        float time = 0;
        Color color = image.color;

        while (time < easeTime)
        {
            color = image.color;
            color.a += 1 / (easeTime / Time.deltaTime);
            image.color = color;

            time += Time.deltaTime;
            yield return null;
        }

        color.a = 1;
        image.color = color;

        yield return new WaitForSeconds(holdTime);

        time = 0;
        while (time < easeTime)
        {
            color = image.color;
            color.a -= 1 / (easeTime / Time.deltaTime);
            image.color = color;

            time += Time.deltaTime;
            yield return null;
        }

        color.a = 0;
        image.color = color;
    }

}
