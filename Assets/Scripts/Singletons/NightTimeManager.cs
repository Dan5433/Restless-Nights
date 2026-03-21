using EditorAttributes;
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

    [Button("Win Night", 36)]
    void EndNight()
    {
        if (!GameManager.Instance.IsPlaying)
            return;

        GameManager.EndNight();
        GameManager.WinNight();
        endTransition.Win();
    }
}
