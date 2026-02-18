using UnityEngine;

public class NightTimeManager : Singleton<NightTimeManager>
{
    [SerializeField] int nightDurationSeconds;
    public float NightTimePassedFraction => Time.timeSinceLevelLoad / nightDurationSeconds;
}
