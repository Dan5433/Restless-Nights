using EditorAttributes;
using UnityEngine;

public class PanicManager : DifficultySingleton<PanicManager>
{
    [SerializeField] float panicIncreasePerActiveTask;
    [SerializeField] float maxPanicBase = LOSE_STATE_PANIC_THRESHOLD * 0.9f;
    [SerializeField] float panicChangeSpeed = 0.5f;
    [SerializeField][DisableInEditMode, DisableInPlayMode] float panicMeter; //0-100
    [SerializeField][DisableInEditMode, DisableInPlayMode] float pressure;
    [SerializeField][DisableInEditMode, DisableInPlayMode] float panicBase;

    const float LOSE_STATE_PANIC_THRESHOLD = 100f;
    const float DIFFICULTY_TO_BASE_PANIC_RATIO = 0.5f;

    private void Update()
    {
        panicBase = difficulty * DIFFICULTY_TO_BASE_PANIC_RATIO;
        panicBase += Mathf.Lerp(0, maxPanicBase - panicBase, NightTimeManager.Instance.NightTimePassedFraction);

        pressure = TasksManager.Instance.ActiveTasksCount * panicIncreasePerActiveTask;

        panicMeter = Mathf.MoveTowards(panicMeter, panicBase + pressure, Time.deltaTime * panicChangeSpeed);

        if (panicMeter > LOSE_STATE_PANIC_THRESHOLD)
            Debug.Log("NIGHT LOSE!");
    }
}
