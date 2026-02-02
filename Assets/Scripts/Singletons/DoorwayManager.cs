using Cinemachine;
using System.Collections;
using UnityEngine;

public class DoorwayManager : DifficultySingleton<DoorwayManager>
{
    [SerializeField] float movementLockExtraTime = 0.1f;
    [SerializeField] PlayerMovement movement;
    [SerializeField] DoorTransition transition;
    [SerializeField] CinemachineConfiner2D cameraConfiner;

    public DoorTransition Transition => transition;

    public int DoorwayChoices
    {
        /*
         * 0 = 1 choice
         * 1-6 = 2 choices
         * 7-13 = 3 choices
         * 14-20 = 4 choices
         */
        get
        {
            if (difficulty == 0)
                return 1;
            if (difficulty <= 6)
                return 2;
            if (difficulty <= 13)
                return 3;
            return 4;
        }
    }

    public static IEnumerator PlayDoorTransition()
    {
        PlayerMovement movement = Instance.movement;
        DoorTransition transition = Instance.transition;

        movement.Locked = true;
        transition.StartCoroutine(transition.FadeTransition());

        yield return new WaitForSeconds(transition.EaseTime + transition.HoldTime + Instance.movementLockExtraTime);

        movement.Locked = false;
    }

    public static void UpdateCameraConfiner(Doorway doorway)
    {
        if (!IsInstanceValid())
            return;

        CinemachineConfiner2D cameraConfiner = Instance.cameraConfiner;
        cameraConfiner.m_BoundingShape2D = doorway.DestinationCameraConfiner;
    }
}
