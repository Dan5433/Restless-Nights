using EditorAttributes;
using UnityEngine;

public class MonsterManager : DifficultySingleton<MonsterManager>
{
    [SerializeField][MinMaxSlider(0, 60)] Vector2Int baseTaskAppearance;

    [SerializeField][DisableInEditMode, DisableInPlayMode] float taskTimer;

    private void Start()
    {
        SetNextTaskTimer();
    }

    private void Update()
    {
        taskTimer -= Time.deltaTime;

        if (taskTimer > 0)
            return;

        TasksManager.TriggerRandomTask();
        SetNextTaskTimer();
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
