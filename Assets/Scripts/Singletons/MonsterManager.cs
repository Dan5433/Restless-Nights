using EditorAttributes;
using System.Collections.Generic;
using UnityEngine;

public class MonsterManager : DifficultySingleton<MonsterManager>
{
    [SerializeField][MinMaxSlider(0, 60)] Vector2Int baseTaskAppearance;
    [SerializeField] Task[] tasks;

    [SerializeField][DisableInEditMode, DisableInPlayMode] float taskTimer;
    [SerializeField][DisableInEditMode, DisableInPlayMode] List<Task> activeTasks;

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

    public static void TaskComplete(Task task)
    {
        if (!IsInstanceValid())
            return;

        Instance.activeTasks.Remove(task);
    }

    void TriggerRandomTask()
    {
        int randomIndex = Random.Range(0, tasks.Length);

        Task task = tasks[randomIndex];
        task.Trigger();
        activeTasks.Add(task);
    }

    void SetNextTaskTimer()
    {
        Vector2 timeRange = baseTaskAppearance - new Vector2Int(difficulty, difficulty);
        float nextTimer = Random.Range(timeRange.x, timeRange.y);

        taskTimer = nextTimer;
    }

    static bool IsInstanceValid()
    {
        if (Instance != null)
            return true;

        Debug.LogError("Monster Manager is not initialized!");
        return false;
    }

    [Button("Skip Task Timer", 36)]
    void SkipTaskTimer()
    {
        taskTimer = 0;
    }
}
