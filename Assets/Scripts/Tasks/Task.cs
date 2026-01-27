using EditorAttributes;
using UnityEngine;

public abstract class Task : MonoBehaviour
{
    [Button("Complete", 36)]
    protected void Complete()
    {
        MonsterManager.TaskComplete(this);
    }

    public abstract void Trigger();
}
