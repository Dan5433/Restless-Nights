using UnityEngine;

public class NightTimeManager : Singleton<NightTimeManager>
{
    [SerializeField] int nightDurationSeconds;

    public int NightDurationSeconds => nightDurationSeconds;
    public float NightTimePassedFraction => Time.timeSinceLevelLoad / nightDurationSeconds;
}
