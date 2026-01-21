using UnityEngine;

public class Doorway : MonoBehaviour
{
    [SerializeField] Doorway destination;
    [SerializeField] Vector3 selfDestinationOffset;

    const string PLAYER_TAG = "Player";

    public Vector3 DestinationPosition => transform.position + selfDestinationOffset;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag(PLAYER_TAG))
            return;

        collision.transform.position = destination.DestinationPosition;
    }
}
