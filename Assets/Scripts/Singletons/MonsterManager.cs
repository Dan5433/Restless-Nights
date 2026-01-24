using EditorAttributes;
using UnityEditor;
using UnityEngine;

public class MonsterManager : MonoBehaviour
{
    public static MonsterManager Instance { get; private set; }

    const int MIN_DIFFICULTY = 0;
    const int MAX_DIFFICULTY = 20;

    [SerializeField][Range(MIN_DIFFICULTY, MAX_DIFFICULTY)] int difficulty;
    [SerializeField][MinMaxSlider(0, 60)] Vector2Int baseTaskAppearance;
    [SerializeField] MonoBehaviour[] taskBehaviours;

    [SerializeField][DisableInEditMode, DisableInPlayMode] float taskTimer;

#if UNITY_EDITOR
    void OnValidate()
    {
        foreach (var task in taskBehaviours)
        {
            if (task == null || task is ITask)
                continue;

            if (EditorApplication.isPlaying)
                EditorApplication.ExitPlaymode();
            else
                Debug.LogError($"{task.name} does not implement ITask", task);
        }
    }
#endif

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Debug.LogWarning($"{GetType().Name} singleton already exists!");
        }
        else
        {

#if UNITY_EDITOR
            OnValidate();
#endif

            Instance = this;
        }
    }

    private void Start()
    {
        SetNextTaskTimer();
    }

    private void Update()
    {
        taskTimer -= Time.deltaTime;

        if (taskTimer > 0)
            return;

        TriggerRandomTask();
        SetNextTaskTimer();
    }

    void TriggerRandomTask()
    {
        int randomIndex = Random.Range(0, taskBehaviours.Length);
        var task = taskBehaviours[randomIndex] as ITask;
        task.Trigger();
    }

    void SetNextTaskTimer()
    {
        Vector2 timeRange = baseTaskAppearance - new Vector2Int(difficulty, difficulty);
        float nextTimer = Random.Range(timeRange.x, timeRange.y);

        taskTimer = nextTimer;
    }

    [Button("Skip Task Timer", 36)]
    void SkipTaskTimer()
    {
        taskTimer = 0;
    }
}
