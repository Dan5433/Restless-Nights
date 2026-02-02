#if UNITY_EDITOR
using UnityEngine;

[RequireComponent(typeof(Transform))]
[RequireComponent(typeof(SpriteRenderer))]
public class PixelAlignmentChecker : MonoBehaviour
{
    static readonly Vector2Int pixelArtSize = new(16, 16);
    static readonly int oddSizeMultiplier = 2;
    Transform runtimeTransform;
    SpriteRenderer runtimeSpriteRenderer;

    private void OnValidate()
    {
        var transform = GetComponent<Transform>();
        var spriteRenderer = GetComponent<SpriteRenderer>();

        CheckAlignment(transform.position, transform.rotation, spriteRenderer.sprite);

        //TODO: Add context to all other debug logs
    }

    private void Awake()
    {
        runtimeTransform = GetComponent<Transform>();
        runtimeSpriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (!runtimeTransform.hasChanged)
            return;

        CheckAlignment(runtimeTransform.position, runtimeTransform.rotation, runtimeSpriteRenderer.sprite);
        transform.hasChanged = false;
    }

    void CheckAlignment(Vector2 position, Quaternion rotation, Sprite sprite)
    {
        if (IsPixelAligned(position, rotation, sprite))
            return;

        Debug.LogWarning($"{gameObject.name} is not pixel aligned!", this);
    }

    bool IsPixelAligned(Vector2 position, Quaternion rotation, Sprite sprite)
    {
        Rect textureRect = sprite.textureRect;
        Vector2 size = textureRect.size;

        if (IsRotationSideways(rotation))
            size = new(size.y, size.x);

        if (size.x % 2 == 0 && position.x * pixelArtSize.x % 1 != 0)
            return false;
        else if (position.x * pixelArtSize.x * oddSizeMultiplier % 1 != 0)
            return false;

        if (size.y % 2 == 0 && position.y * pixelArtSize.y % 1 != 0)
            return false;
        else if (position.y * pixelArtSize.y * oddSizeMultiplier % 1 != 0)
            return false;

        return true;
    }

    bool IsRotationSideways(Quaternion rotation)
    {
        return rotation.eulerAngles.z == 90 || rotation.eulerAngles.z == 270;
    }
}
#endif