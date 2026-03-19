using UnityEngine;

public class NightTimeManager : Singleton<NightTimeManager>
{
    [SerializeField] int nightDurationSeconds;
    [SerializeField] EndTransition endTransition;
    public float NightTimePassedFraction => Time.timeSinceLevelLoad / nightDurationSeconds;

    void Start()
    {
        Invoke(nameof(EndNight), nightDurationSeconds);
    }

    void EndNight()
    {
        if (!GameManager.Instance.IsPlaying)
            return;

        endTransition.Win();
    }
}
