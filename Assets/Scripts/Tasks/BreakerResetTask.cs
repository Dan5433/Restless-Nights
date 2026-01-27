using UnityEngine;
using UnityEngine.UI;

public class BreakerResetTask : MonoBehaviour, ITask
{
    [SerializeField] Slider mainBreaker;
    [SerializeField] Slider[] roomBreakers;
    public void Trigger()
    {
        Debug.Log("Circuit breaker reset task!");
    }
}
