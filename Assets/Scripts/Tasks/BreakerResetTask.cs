using UnityEngine;
using UnityEngine.UI;

public class BreakerResetTask : Task
{
    [SerializeField] Slider mainBreaker;
    [SerializeField] Slider[] roomBreakers;
    public override void Trigger()
    {
        Debug.Log("Circuit breaker reset task!");
    }
}
