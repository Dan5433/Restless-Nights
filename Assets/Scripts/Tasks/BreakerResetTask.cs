using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BreakerResetTask : Task
{
    [SerializeField] Slider mainBreaker;
    [SerializeField] Slider[] roomBreakers;

    const int MIN_BREAKERS = 1;

    protected override void TriggerInternal()
    {
        RandomizeBreakers();
    }

    void RandomizeBreakers()
    {
        List<int> breakers = new(roomBreakers.Length);
        for (int i = 0; i < roomBreakers.Length; i++)
            breakers.Add(i);

        int maxExtraBreakers = roomBreakers.Length - MIN_BREAKERS;

        int extraDisabledBreakers = Mathf.RoundToInt(TasksManager.Instance.DifficultyFraction * maxExtraBreakers);
        int disabledBreakers = MIN_BREAKERS + extraDisabledBreakers;

        for (int i = 0; i < disabledBreakers; i++)
        {
            int randomIndex = Random.Range(0, breakers.Count);

            int breaker = breakers[randomIndex];
            roomBreakers[breaker].value = 0;
            breakers.Remove(breaker);
        }
    }

    bool AllRoomBreakersEnabled()
    {
        foreach (Slider breaker in roomBreakers)
        {
            if (breaker.value == 0)
                return false;
        }

        return true;
    }

    public void ChangeMainBreakerState(float sliderValue)
    {
        if (!active || sliderValue == 0 || !AllRoomBreakersEnabled())
            return;

        Complete();
    }
}
