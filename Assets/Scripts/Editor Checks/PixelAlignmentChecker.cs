#if UNITY_EDITOR
using UnityEngine;

[RequireComponent(typeof(Transform))]
public class PixelAlignmentChecker : MonoBehaviour
{
    readonly Vector2Int pixelArtSize = new(32, 32);

    private void OnValidate()
    {
        var transform = GetComponent<Transform>();

        if (!IsPixelAligned(transform.position))
            Debug.LogWarning($"{gameObject.name} is not pixel aligned!", this);

        //TODO: Add context to all other debug logs
    }

    bool IsPixelAligned(Vector2 position)
    {
        return (position.x * pixelArtSize.x) % 1 == 0
            && (position.y * pixelArtSize.y) % 1 == 0;
    }
}
#endif