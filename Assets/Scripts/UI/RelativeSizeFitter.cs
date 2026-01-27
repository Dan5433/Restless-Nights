using EditorAttributes;
using UnityEngine;
using UnityEngine.UI;

public class RelativeSizeFitter : MonoBehaviour, ILayoutSelfController
{
    [SerializeField] RectTransform relativeTransform;
    [SerializeField] bool controlWidth = false;
    [SerializeField, EnableField(nameof(controlWidth))][Range(0.001f, 100f)] float widthMultiplier;
    [SerializeField] bool controlHeight = false;
    [SerializeField, EnableField(nameof(controlHeight))][Range(0.001f, 100f)] float heightMultiplier;

    void UpdateRectTransform()
    {
        var transform = GetComponent<RectTransform>();

        Vector2 size = transform.sizeDelta;

        if (controlWidth)
            size.x = relativeTransform.sizeDelta.x * widthMultiplier;
        if (controlHeight)
            size.y = relativeTransform.sizeDelta.y * heightMultiplier;

        transform.sizeDelta = size;
    }

    void SetDirty()
    {
        if (!isActiveAndEnabled)
            return;

        LayoutRebuilder.MarkLayoutForRebuild(GetComponent<RectTransform>());
    }

    private void OnValidate()
    {
        SetDirty();
    }

    private void OnRectTransformDimensionsChange()
    {
        SetDirty();
    }

    public void SetLayoutHorizontal()
    {
        UpdateRectTransform();
    }

    public void SetLayoutVertical()
    {
        UpdateRectTransform();
    }
}
