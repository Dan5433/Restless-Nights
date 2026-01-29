using EditorAttributes;
using System.Collections.Generic;
using UnityEngine;

public class TasksManager : DifficultySingleton<TasksManager>
{
    [SerializeField] Task[] tasks;
    [SerializeField][DisableInEditMode, DisableInPlayMode] List<Task> activeTasks;

    public float DifficultyFraction => (float)difficulty / MAX_DIFFICULTY;
    public int ActiveTasksCount => activeTasks.Count;

    public static void TriggerRandomTask()
    {
        if (!IsInstanceValid())
            return;

        int randomIndex = Random.Range(0, Instance.tasks.Length);

        Task task = Instance.tasks[randomIndex];
        task.Trigger();
        Instance.activeTasks.Add(task);
    }

    public static void TaskComplete(Task task)
    {
        if (!IsInstanceValid())
            return;

        Instance.activeTasks.Remove(task);
    }

}
