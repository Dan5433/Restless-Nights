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

    [SerializeField][MinMaxSlider(0, 1)] Vector2 audioVolumeRange;
    [SerializeField][MinMaxSlider(0.5f, 1.5f)] Vector2 audioPitchRange;
    [SerializeField] AudioSource breathingAudio;

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
        float volume = Mathf.Lerp(audioVolumeRange.x, audioVolumeRange.y, panicMeter / LOSE_STATE_PANIC_THRESHOLD);
        float pitch = Mathf.Lerp(audioPitchRange.x, audioPitchRange.y, panicMeter / LOSE_STATE_PANIC_THRESHOLD);

        breathingAudio.volume = volume;
        breathingAudio.pitch = pitch;
    }
}
