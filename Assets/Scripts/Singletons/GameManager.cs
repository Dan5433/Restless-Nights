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

        if (loadedNight != null)
            night = loadedNight;

        isPlaying = true;
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

    public static void WinNight()
    {
        if (!IsInstanceValid())
            return;

        TitleScreen.WinNight(loadedNight);
    }
}
