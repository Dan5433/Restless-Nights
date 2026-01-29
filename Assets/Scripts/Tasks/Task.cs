using EditorAttributes;
using UnityEngine;

public abstract class Task : MonoBehaviour
{
    [SerializeField, DisableInEditMode, DisableInPlayMode] protected bool active = false;

    public bool Active => active;

    [Button("Complete", 36)]
    protected void Complete()
    {
        Debug.Log($"{GetType().Name} complete!");
        active = false;

        TasksManager.TaskComplete(this);
    }

    [Button("Trigger", 36)]
    public void Trigger()
    {
        Debug.Log($"{GetType().Name} triggered!");
        active = true;

        TriggerInternal();
    }

    protected abstract void TriggerInternal();
}
