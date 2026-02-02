using EditorAttributes;
using System.Collections.Generic;
using UnityEngine;
public class LightSwitchTask : Task
{
    [SerializeField] LightSwitch[] lightSwitches;
    [SerializeField] float maxDisabledSwitchesRatio = 0.5f;
    [SerializeField][DisableInEditMode, DisableInPlayMode] List<LightSwitch> disabledSwitches;

    const int MIN_SWITCHES = 1;

    protected override void TriggerInternal()
    {
        List<int> switches = new(lightSwitches.Length);
        for (int i = 0; i < lightSwitches.Length; i++)
            switches.Add(i);

        float difficulty = TasksManager.Instance.DifficultyFraction;

        float maxSwitches = lightSwitches.Length * maxDisabledSwitchesRatio;

        int disabledSwitchesAmount = Mathf.RoundToInt(
            Mathf.Lerp(MIN_SWITCHES, maxSwitches, difficulty));

        for (int i = 0; i < disabledSwitchesAmount; i++)
        {
            int randomIndex = Random.Range(0, switches.Count);

            int switchIndex = switches[randomIndex];

            LightSwitch lightSwitch = lightSwitches[switchIndex];
            disabledSwitches.Add(lightSwitch);
            lightSwitch.TurnOff();

            switches.Remove(switchIndex);
        }
    }

    protected override void Complete()
    {
        base.Complete();

        disabledSwitches.Clear();
    }

    public void LightSwitchStateUpdate()
    {
        foreach (LightSwitch lightSwitch in disabledSwitches)
        {
            if (!lightSwitch.IsOn)
                return;
        }

        Complete();
    }
}