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
    bool hasTaskAppeared;

    public float DifficultyFraction => (float)difficulty / MAX_DIFFICULTY;
    public int ActiveTasksCount => tasks.Length - availableTasks.Count;

    protected override void Awake()
    {
        base.Awake();

        availableTasks = tasks.ToList();
        difficulty = GameManager.Instance.Night.NightDifficulty.tasks;
    }

    public IEnumerator TriggerRandomTask()
    {
        if (availableTasks.Count == 0)
        {
            Debug.Log("No available tasks");
        }
        else
        {
            int randomIndex = Random.Range(0, availableTasks.Count);

            Task task = availableTasks[randomIndex];

            yield return task.Trigger();

            availableTasks.Remove(task);
            PlayTaskReactionAudio(taskAppearSfx);

            if (!hasTaskAppeared)
            {
                yield return new WaitWhile(() => taskReactionAudio.isPlaying);
                DisplayFirstTaskMessage();
                hasTaskAppeared = true;
            }
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
        taskReactionAudio.volume = Mathf.Lerp(
            taskReactionAudioVolumeRange.x,
            taskReactionAudioVolumeRange.y,
            PanicManager.Instance.PanicFraction);

        taskReactionAudio.PlayOneShotWithRandomPitch(clip);
    }

    void DisplayFirstTaskMessage()
    {
        foreach (DialogManager.Message message in GameManager.Instance.Night.FirstTaskMessages)
            DialogManager.QueueMessage(message.text, message.fontStyle);
    }
}
