using EditorAttributes;
using UnityEngine;

public abstract class Task : MonoBehaviour
{
    [SerializeField] protected bool active = false;

    [Button("Complete", 36)]
    protected void Complete()
    {
        Debug.Log($"{GetType().Name} complete!");
        active = false;

        MonsterManager.TaskComplete(this);
    }

    [Button("Trigger", 36)]
    public void Trigger()
    {
        Debug.Log($"{GetType().Name} triggered!");
        active = true;

        TriggerInternal();
    }

    [Button("Trigger Internal Test", 36)]
    protected abstract void TriggerInternal();
}
