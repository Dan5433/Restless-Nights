using EditorAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockingDoorsTask : Task
{
    [SerializeField] Doorway[] doors;
    [SerializeField] float maxOpenedDoorsRatio = 0.5f;
    [SerializeField][DisableInEditMode, DisableInPlayMode] List<Doorway> openedDoors;
    const int MIN_DOORS = 1;

    protected override IEnumerator TriggerTaskCoroutine()
    {
        List<int> doorIndexes = new(doors.Length);
        for (int i = 0; i < doors.Length; i++)
            doorIndexes.Add(i);

        float difficulty = TasksManager.Instance.DifficultyFraction;

        float maxSwitches = doors.Length * maxOpenedDoorsRatio;

        int disabledSwitchesAmount = Mathf.RoundToInt(
            Mathf.Lerp(MIN_DOORS, maxSwitches, difficulty));

        for (int i = 0; i < disabledSwitchesAmount; i++)
        {
            int randomIndex = Random.Range(0, doorIndexes.Count);

            int switchIndex = doorIndexes[randomIndex];

            Doorway door = doors[switchIndex];
            door.OpenDoor();
            openedDoors.Add(door);

            doorIndexes.Remove(switchIndex);

            yield return new WaitWhile(() => door.IsAudioPlaying);
        }
    }

    private void Update()
    {
        if (openedDoors.Count == 0)
            return;

        CheckLockedDoors();
    }

    void CheckLockedDoors()
    {
        foreach (Doorway door in openedDoors)
        {
            if (!door.IsClosed)
                return;
        }

        Complete();
    }

    protected override void Complete()
    {
        base.Complete();

        openedDoors.Clear();
    }
}
