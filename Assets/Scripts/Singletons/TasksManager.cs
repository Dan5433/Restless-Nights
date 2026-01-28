using UnityEngine;

public class TasksManager : MonoBehaviour
{
    public static TasksManager Instance { get; private set; }

    const int MIN_DIFFICULTY = 0;
    const int MAX_DIFFICULTY = 20;

    [SerializeField][Range(MIN_DIFFICULTY, MAX_DIFFICULTY)] int difficulty;

    public int Difficulty => difficulty;
    public static int MaxDifficulty => MAX_DIFFICULTY;

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
