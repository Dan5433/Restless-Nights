using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] bool isPlaying;
    [SerializeField] NightSO night;

    static NightSO loadedNight;

    public bool IsPlaying => isPlaying;
    public NightSO Night => night;

    protected override void Awake()
    {
        base.Awake();

        isPlaying = true;
        night = loadedNight;
    }

    public static void EndNight()
    {
        if (!IsInstanceValid())
            return;

        Instance.isPlaying = false;
    }

    public static void LoadNight(NightSO night)
    {
        loadedNight = night;
    }
}
