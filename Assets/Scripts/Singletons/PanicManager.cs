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
    }
}
