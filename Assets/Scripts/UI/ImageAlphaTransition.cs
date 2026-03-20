using EditorAttributes;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ImageAlphaTransition : MonoBehaviour
{
    [SerializeField] Image image;
    [SerializeField] float easeTime = 1f;
    [SerializeField] float holdTime = 0.25f;
    [SerializeField] float minAlpha = 0, maxAlpha = 1;
    [SerializeField] bool playOnStart, loop;

    public float EaseTime => easeTime;
    public float HoldTime => holdTime;

    void Start()
    {
        if (playOnStart)
            StartCoroutine(Play());
    }

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
        Color color = image.color;

        while (time < easeTime)
        {
            color = image.color;
            color.a = Mathf.Max(Mathf.Lerp(minAlpha, maxAlpha, time / easeTime), color.a);
            image.color = color;

            time += Time.deltaTime;
            yield return null;
        }

        color.a = maxAlpha;
        image.color = color;

        yield return new WaitForSeconds(holdTime);

        time = 0;
        while (time < easeTime)
        {
            color = image.color;
            color.a = Mathf.Lerp(maxAlpha, minAlpha, time / easeTime);
            image.color = color;

            time += Time.deltaTime;
            yield return null;
        }

        color.a = minAlpha;
        image.color = color;

        if (loop)
            StartCoroutine(Play());
    }
}
