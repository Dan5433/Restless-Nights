#if UNITY_EDITOR
using UnityEngine;

[RequireComponent(typeof(Transform))]
[RequireComponent(typeof(SpriteRenderer))]
public class PixelAlignmentChecker : MonoBehaviour
{
    const string OUTLINE_MATERIAL_NAME = "Sprite Lit Outline";
    const int OUTLINE_WIDTH = 1;
    static readonly Vector2Int pixelArtSize = new(16, 16);
    static readonly int oddSizeMultiplier = 2;
    Transform runtimeTransform;
    SpriteRenderer runtimeSpriteRenderer;

    private void OnValidate()
    {
        var transform = GetComponent<Transform>();
        var spriteRenderer = GetComponent<SpriteRenderer>();

        CheckAlignment(transform.position, transform.rotation, spriteRenderer);
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

        CheckAlignment(runtimeTransform.position, runtimeTransform.rotation, runtimeSpriteRenderer);
        runtimeTransform.hasChanged = false;
    }

    void CheckAlignment(Vector2 position, Quaternion rotation, SpriteRenderer spriteRenderer)
    {
        if (IsPixelAligned(position, rotation, spriteRenderer.sprite, spriteRenderer.sharedMaterial.name == OUTLINE_MATERIAL_NAME))
            return;

        Debug.LogWarning($"{gameObject.name} is not pixel aligned!", this);
    }

    bool IsPixelAligned(Vector2 position, Quaternion rotation, Sprite sprite, bool outlinedSprite)
    {
        Rect textureRect = sprite.textureRect;
        Vector2 size;
        if (outlinedSprite)
            size = (sprite.pivot - new Vector2(OUTLINE_WIDTH, OUTLINE_WIDTH)) * 2;
        else
            size = textureRect.size;

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