using EditorAttributes;
using Extensions;
using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Doorway : Interactable
{
    [SerializeField] bool isClosed = true;
    [SerializeField] Doorway currentDestination;
    [SerializeField] Doorway[] destinations = new Doorway[4];
    [SerializeField] Vector3 destinationSelfOffset;
    [SerializeField] PolygonCollider2D destinationSelfCameraConfiner;
    [SerializeField] GameObject doorFrame;
    Light2D doorwayLight;

    const string PLAYER_TAG = "Player";

    static readonly Vector3 gizmoDestinationOccupiedIndicatorSizeChange = new(0.25f, 0.25f);
    static readonly Vector3 gizmoDestinationChosenIndicatorSize = new(0.3f, 0.3f);
    static readonly Vector3 gizmoDestinationChosenIndicatorOffset =
        gizmoDestinationChosenIndicatorSize + new Vector3(0.1f, 0.1f);

    public Vector3 DestinationPosition => transform.position + destinationSelfOffset;
    public PolygonCollider2D DestinationCameraConfiner => destinationSelfCameraConfiner;
    public Light2D DoorwayLight => doorwayLight;
    public Doorway CurrentDestination => currentDestination;
    public bool IsClosed => isClosed;
    protected override bool CanInteract => !isClosed;

    public override AudioClip InteractSFX => AudioManager.Instance.CloseDoor;

    protected override void Awake()
    {
        base.Awake();

        currentDestination = destinations[0];
        doorwayLight = GetComponentInChildren<Light2D>();
    }

    private void Start()
    {
        UpdateLightIntensity();
        UpdateDoorFrameSprite();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag(PLAYER_TAG))
            return;

        if (DoorwayManager.Instance.DoorwayChoices > 1)
            RollChanceToSwitchDoorway();

        StartCoroutine(Transition(collision));
    }

    IEnumerator Transition(Collider2D collision)
    {
        DoorwayManager.Instance.StartCoroutine(
            DoorwayManager.PlayDoorTransition());

        bool isCurrentDoorClosed = IsClosed;
        bool isCurrentDestinationClosed = currentDestination.IsClosed;

        OpenDoor();
        currentDestination.OpenDoor();

        yield return new WaitForSeconds(DoorwayManager.Instance.Transition.EaseTime);

        collision.transform.position = currentDestination.DestinationPosition;
        DoorwayManager.UpdateCameraConfiner(currentDestination);

        yield return new WaitForSeconds(DoorwayManager.Instance.Transition.HoldTime);

        if (isCurrentDoorClosed)
            PlayInteractSFX();
        if (isCurrentDestinationClosed)
            currentDestination.PlayInteractSFX();

        yield return new WaitForSeconds(DoorwayManager.Instance.Transition.EaseTime);

        if (isCurrentDoorClosed)
            CloseDoor();
        if (isCurrentDestinationClosed)
            currentDestination.CloseDoor();

        LightManager.UpdateDoorwayLightsAfterMovingRooms(this, currentDestination);
    }

    public void OpenDoor()
    {
        if (!isClosed)
            return;

        audioSource.PlayOneShotWithRandomPitch(AudioManager.Instance.OpenDoor);

        isClosed = false;

        UpdateLightIntensity();
        UpdateDoorFrameSprite();
        UpdateMaterialPropertyBlock();
    }

    protected override bool InteractInternal()
    {
        CloseDoor();
        return true;
    }

    void CloseDoor()
    {
        RaycastExit();

        isClosed = true;

        UpdateLightIntensity();
        UpdateDoorFrameSprite();
    }

    void RollChanceToSwitchDoorway()
    {
        int choiceNumber = Random.Range(DoorwayManager.MIN_DIFFICULTY, DoorwayManager.MAX_DIFFICULTY);
        if (choiceNumber <= DoorwayManager.Instance.Difficulty)
        {
            SwitchDoorway(DoorwayManager.Instance.DoorwayChoices);
        }
    }

    [Button("Roll Doorway Destination", 36)]
    void SwitchDoorway(int choiceAmount)
    {
        int randomIndex;
        //ensure chosen doorway cannot be the same as current one
        do
            randomIndex = Random.Range(0, choiceAmount);
        while (destinations[randomIndex] == currentDestination);

        currentDestination = destinations[randomIndex];
    }

    void UpdateLightIntensity()
    {
        doorwayLight.intensity = isClosed
            ? DoorwayManager.Instance.ClosedDoorLightIntensity
            : DoorwayManager.Instance.OpenDoorLightIntensity;
    }

    void UpdateDoorFrameSprite()
    {
        doorFrame.GetComponent<SpriteRenderer>().sprite = isClosed
            ? DoorwayManager.Instance.ClosedDoorSprite
            : DoorwayManager.Instance.OpenDoorSprite;
    }

    void OnDrawGizmosSelected()
    {
        //random doorways per difficulty
        if (destinations.Length < 1 || destinations[0] == null)
            return;
        Gizmos.color = Color.white;
        Gizmos.DrawLine(transform.position, destinations[0].transform.position);

        if (destinations.Length < 2 || destinations[1] == null)
            return;
        Gizmos.color = Color.green;
        DrawGizmoIndicators(destinations[1].transform, 1);

        if (destinations.Length < 3 || destinations[2] == null)
            return;
        Gizmos.color = Color.Lerp(Color.yellow, Color.red, 0.4f); // why does Color not have orange?
        DrawGizmoIndicators(destinations[2].transform, 2);

        if (destinations.Length < 4 || destinations[3] == null)
            return;
        Gizmos.color = Color.red;
        DrawGizmoIndicators(destinations[3].transform, 3);
    }

    void DrawGizmoIndicators(Transform destination, int choiceIndex)
    {
        //draw line to destination
        Gizmos.DrawLine(transform.position, destination.position);

        //draw squares to show chosen destination choices
        Gizmos.DrawCube(transform.position - gizmoDestinationChosenIndicatorOffset * destinations.Length / 2
            + gizmoDestinationChosenIndicatorOffset * choiceIndex,
            gizmoDestinationChosenIndicatorSize);

        //draw wire squares at destination choices
        Gizmos.DrawWireCube(destination.position,
            destination.localScale - gizmoDestinationOccupiedIndicatorSizeChange * choiceIndex);
    }
}
