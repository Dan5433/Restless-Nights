using UnityEngine;

public class TimeManager : Singleton<TimeManager>
{
    [SerializeField] int nightDurationSeconds;

    public int NightDurationSeconds => nightDurationSeconds;
    public float NightTimePassedFraction => Time.timeSinceLevelLoad / nightDurationSeconds;
}
