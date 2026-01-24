using System;
using UnityEngine;

[Serializable]
public class BreakerResetTask : MonoBehaviour, ITask
{
    public void Trigger()
    {
        Debug.Log("Circuit breaker reset task!");
    }
}
