using EditorAttributes;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TasksManager : DifficultySingleton<TasksManager>
{
    [SerializeField] Task[] tasks;
    [SerializeField][DisableInEditMode, DisableInPlayMode] List<Task> availableTasks;
    [SerializeField] AudioSource completeTaskAudio;
    [SerializeField][MinMaxSlider(0, 1)] Vector2 completeTaskAudioVolumeRange;

    public float DifficultyFraction => (float)difficulty / MAX_DIFFICULTY;
    public int ActiveTasksCount => tasks.Length - availableTasks.Count;

    protected override void Awake()
    {
        base.Awake();

        availableTasks = tasks.ToList();
    }

    public static void TriggerRandomTask()
    {
        if (!IsInstanceValid())
            return;


        if (Instance.availableTasks.Count == 0)
        {
            Debug.Log("No available tasks");
            return;
        }

        int randomIndex = Random.Range(0, Instance.availableTasks.Count);

        Task task = Instance.availableTasks[randomIndex];
        task.Trigger();
        Instance.availableTasks.Remove(task);
    }

    public static void TaskComplete(Task task)
    {
        if (!IsInstanceValid())
            return;

        Instance.availableTasks.Add(task);

        Instance.completeTaskAudio.volume = Mathf.Lerp(
            Instance.completeTaskAudioVolumeRange.x,
            Instance.completeTaskAudioVolumeRange.y,
            PanicManager.Instance.PanicFraction);

        Instance.completeTaskAudio.PlayOneShot(Instance.completeTaskAudio.clip);
    }
}
