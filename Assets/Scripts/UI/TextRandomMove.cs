using EditorAttributes;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(RectTransform))]
public class TextRandomMove : MonoBehaviour
{
    [SerializeField][MinMaxSlider(0f, 5f)] Vector2 moveTimeRange, holdTimeRange;
    [SerializeField][MinMaxSlider(-20f, 20f)] Vector2 xMoveRange, yMoveRange;
    float moveTime;
    Vector2 start, target;
    RectTransform rectTransform;

    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    void Start()
    {
        RollMoveValues();
        StartCoroutine(MoveTowardsTarget());
    }

    void RollMoveValues()
    {
        moveTime = Random.Range(moveTimeRange.x, moveTimeRange.y);

        target = new(
            Random.Range(xMoveRange.x, xMoveRange.y),
            Random.Range(yMoveRange.x, yMoveRange.y));
    }

    IEnumerator MoveTowardsTarget()
    {
        start = rectTransform.anchoredPosition;
        float time = 0;

        while (time < moveTime)
        {
            rectTransform.anchoredPosition = Vector2.Lerp(start, target, time / moveTime);

            time += Time.deltaTime;
            yield return null;
        }

        rectTransform.anchoredPosition = target;

        RollMoveValues();
        StartCoroutine(MoveTowardsTarget());
    }
}
