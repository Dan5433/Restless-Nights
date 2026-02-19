using EditorAttributes;
using System.Collections;
using UnityEngine;

public abstract class Task : MonoBehaviour
{
    [SerializeField, DisableInEditMode, DisableInPlayMode] protected bool active = false;

    public bool Active => active;

    [Button("Complete", 36)]
    protected virtual void Complete()
    {
        Debug.Log($"{GetType().Name} complete!");
        active = false;

        TasksManager.TaskComplete(this);
    }

    [Button("Trigger", 36)]
    public IEnumerator Trigger()
    {
        Debug.Log($"{GetType().Name} triggered!");
        active = true;

        yield return TriggerTaskCoroutine();
    }

    protected abstract IEnumerator TriggerTaskCoroutine();
}
