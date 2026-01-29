using UnityEngine;

public abstract class Singleton<T> : MonoBehaviour
    where T : MonoBehaviour
{
    public static T Instance { get; private set; }

    protected virtual void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Debug.LogWarning($"{nameof(T)} singleton already exists! Destroying this instance...");
            Destroy(this);
        }
        else
        {
            Instance = (T)(MonoBehaviour)this;
        }
    }
    protected static bool IsInstanceValid()
    {
        if (Instance != null)
            return true;

        Debug.LogError($"{nameof(T)} is not initialized!");
        return false;
    }
}
