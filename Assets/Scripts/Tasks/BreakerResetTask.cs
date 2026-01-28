using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BreakerResetTask : Task
{
    [SerializeField] Slider mainBreaker;
    [SerializeField] Slider[] roomBreakers;

    const int MIN_BREAKERS = 1;

    public override void Trigger()
    {
        Debug.Log("Circuit breaker reset task!");
        RandomizeBreakers();
    }

    void RandomizeBreakers()
    {
        List<int> breakers = new(roomBreakers.Length);
        for (int i = 0; i < roomBreakers.Length; i++)
            breakers.Add(i);

        int difficulty = TasksManager.Instance.Difficulty;
        float difficultFraction = (difficulty / TasksManager.MaxDifficulty);
        int disabledBreakers = Mathf.RoundToInt(MIN_BREAKERS + difficultFraction * (roomBreakers.Length - MIN_BREAKERS));

        for (int i = 0; i < disabledBreakers; i++)
        {
            int randomIndex = Random.Range(0, breakers.Count);

            int breaker = breakers[randomIndex];
            roomBreakers[breaker].value = 0;
            breakers.Remove(breaker);
        }
    }
}
