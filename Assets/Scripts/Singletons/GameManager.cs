using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] bool isPlaying;

    public bool IsPlaying => isPlaying;

    protected override void Awake()
    {
        base.Awake();

        isPlaying = true;
    }

    public static void EndNight()
    {
        if (!IsInstanceValid())
            return;

        Instance.isPlaying = false;
    }
}
