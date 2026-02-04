using System.Collections.Generic;
using UnityEngine;

public class LockingDoorsTask : Task
{
    [SerializeField] Doorway[] doors;
    [SerializeField] float maxOpenedDoorsRatio = 0.5f;
    const int MIN_DOORS = 1;

    protected override void TriggerInternal()
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

            doors[switchIndex].OpenDoor();

            doorIndexes.Remove(switchIndex);
        }
    }
}
