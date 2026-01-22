using UnityEngine;

public class DoorwayManager : MonoBehaviour
{
    public static DoorwayManager Instance { get; private set; }

    const int MIN_DIFFICULTY = 0;
    const int MAX_DIFFICULTY = 20;

    [SerializeField][Range(MIN_DIFFICULTY, MAX_DIFFICULTY)] int difficulty;

    public int Difficulty => difficulty;
    public static int MinDifficulty => MIN_DIFFICULTY;
    public static int MaxDifficulty => MAX_DIFFICULTY;

    public int DoorwayChoices
    {
        get
        {
            if (difficulty == 0)
                return 1;
            if (difficulty <= 6)
                return 2;
            if (difficulty <= 13)
                return 3;
            return 4;
        }
    }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Debug.LogWarning($"{GetType().Name} singleton already exists!");
        }
        else
        {
            Instance = this;
        }
    }
}
