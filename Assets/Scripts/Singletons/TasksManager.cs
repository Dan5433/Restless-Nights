using EditorAttributes;
using Extensions;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TasksManager : DifficultySingleton<TasksManager>
{
    [SerializeField] Task[] tasks;
    [SerializeField][DisableInEditMode, DisableInPlayMode] List<Task> availableTasks;
    [SerializeField] AudioSource taskReactionAudio;
    [SerializeField] AudioClip completeTaskSfx;
    [SerializeField] AudioClip taskAppearSfx;
    [SerializeField][MinMaxSlider(0, 1)] Vector2 taskReactionAudioVolumeRange;
    [SerializeField] float completeAudioDelay;

    public float DifficultyFraction => (float)difficulty / MAX_DIFFICULTY;
    public int ActiveTasksCount => tasks.Length - availableTasks.Count;

    protected override void Awake()
    {
        base.Awake();

        availableTasks = tasks.ToList();
    }

    public IEnumerator TriggerRandomTask()
    {
        if (Instance.availableTasks.Count == 0)
        {
            Debug.Log("No available tasks");
        }
        else
        {
            int randomIndex = Random.Range(0, Instance.availableTasks.Count);

            Task task = Instance.availableTasks[randomIndex];

            yield return Instance.StartCoroutine(task.Trigger());

            Instance.availableTasks.Remove(task);
            Instance.PlayTaskReactionAudio(Instance.taskAppearSfx);
        }
    }

    public static void TaskComplete(Task task)
    {
        if (!IsInstanceValid())
            return;

        Instance.availableTasks.Add(task);
        Instance.Invoke(nameof(PlayCompleteAudio), Instance.completeAudioDelay);
    }

    void PlayCompleteAudio()
    {
        PlayTaskReactionAudio(completeTaskSfx);
    }

    void PlayTaskReactionAudio(AudioClip clip)
    {
        Instance.taskReactionAudio.volume = Mathf.Lerp(
            Instance.taskReactionAudioVolumeRange.x,
            Instance.taskReactionAudioVolumeRange.y,
            PanicManager.Instance.PanicFraction);

        Instance.taskReactionAudio.PlayOneShotWithRandomPitch(clip);
    }
}
