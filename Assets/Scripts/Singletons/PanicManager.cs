using EditorAttributes;
using UnityEngine;

public class PanicManager : DifficultySingleton<PanicManager>
{
    [SerializeField] float panicIncreasePerActiveTask;
    [SerializeField] float maxPanicBase;
    [SerializeField] float panicChangeSpeed;
    [SerializeField] float maxPanicThreshold; //fraction of night time at which panic will reach max
    [SerializeField][DisableInEditMode, DisableInPlayMode] float panicMeter; //0-100
    [SerializeField][DisableInEditMode, DisableInPlayMode] float pressure;
    [SerializeField][DisableInEditMode, DisableInPlayMode] float panicBase;

    [Space(32)]

    [SerializeField] AudioSource breathingAudio;
    [SerializeField][MinMaxSlider(0, 1)] Vector2 breathingVolumeRange;
    [SerializeField][MinMaxSlider(0.5f, 1.5f)] Vector2 breathingPitchRange;

    [SerializeField] AudioSource heartbeatAudio;
    [SerializeField][MinMaxSlider(0, 1)] Vector2 heartbeatVolumeRange;
    [SerializeField][MinMaxSlider(0.5f, 1.5f)] Vector2 heartbeatPitchRange;

    const float LOSE_STATE_PANIC_THRESHOLD = 100f;
    const float DIFFICULTY_TO_BASE_PANIC_RATIO = 0.5f;

    private void Update()
    {
        float maxAddedPanic = maxPanicBase - MAX_DIFFICULTY * DIFFICULTY_TO_BASE_PANIC_RATIO;

        panicBase = difficulty * DIFFICULTY_TO_BASE_PANIC_RATIO;
        panicBase += Mathf.Lerp(0, maxAddedPanic, NightTimeManager.Instance.NightTimePassedFraction / maxPanicThreshold);

        pressure = TasksManager.Instance.ActiveTasksCount * panicIncreasePerActiveTask;

        panicMeter = Mathf.MoveTowards(panicMeter, panicBase + pressure, Time.deltaTime * panicChangeSpeed);

        if (panicMeter > LOSE_STATE_PANIC_THRESHOLD)
            Debug.Log("NIGHT LOSE!");

        UpdateAudio();
    }

    void UpdateAudio()
    {
        float panicFraction = panicMeter / LOSE_STATE_PANIC_THRESHOLD;

        breathingAudio.volume = Mathf.Lerp(breathingVolumeRange.x, breathingVolumeRange.y, panicFraction);
        breathingAudio.pitch = Mathf.Lerp(breathingPitchRange.x, breathingPitchRange.y, panicFraction);

        heartbeatAudio.volume = Mathf.Lerp(heartbeatVolumeRange.x, heartbeatVolumeRange.y, panicFraction);
        heartbeatAudio.pitch = Mathf.Lerp(heartbeatPitchRange.x, heartbeatPitchRange.y, panicFraction);
    }
}
