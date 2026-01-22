using UnityEngine;

public class Doorway : MonoBehaviour
{
    [SerializeField] Doorway currentDestination;
    [SerializeField] Doorway[] destinations = new Doorway[4];
    [SerializeField] Vector3 selfDestinationOffset;

    const string PLAYER_TAG = "Player";

    public Vector3 DestinationPosition => transform.position + selfDestinationOffset;

    void Awake()
    {
        currentDestination = destinations[0];
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag(PLAYER_TAG))
            return;

        collision.transform.position = currentDestination.DestinationPosition;

        if (DoorwayManager.Instance.Difficulty == 0)
            return;

        RollChanceToSwitchDoorway();
    }

    void RollChanceToSwitchDoorway()
    {
        int choiceNumber = Random.Range(DoorwayManager.MinDifficulty, DoorwayManager.MaxDifficulty);
        if (choiceNumber <= DoorwayManager.Instance.Difficulty)
        {
            SwitchDoorway();
        }
    }

    void SwitchDoorway()
    {
        int choiceAmount = DoorwayManager.Instance.DoorwayChoices;

        int randomIndex;
        //ensure chosen doorway cannot be the same as current one
        do
            randomIndex = Random.Range(0, choiceAmount);
        while (destinations[randomIndex] == currentDestination);

        currentDestination = destinations[randomIndex];
    }

    private void OnDrawGizmos()
    {
        //random doorways per difficulty
        if (destinations.Length < 1 || destinations[0] == null)
            return;
        Gizmos.color = Color.white;
        Gizmos.DrawLine(transform.position, destinations[0].transform.position);

        if (destinations.Length < 2 || destinations[1] == null)
            return;
        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position, destinations[1].transform.position);

        if (destinations.Length < 3 || destinations[2] == null)
            return;
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position, destinations[2].transform.position);

        if (destinations.Length < 4 || destinations[3] == null)
            return;
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, destinations[3].transform.position);
    }
}
