using UnityEngine;

public abstract class DifficultySingleton<T> : Singleton<T>
    where T : MonoBehaviour
{
    public const int MIN_DIFFICULTY = 0;
    public const int MAX_DIFFICULTY = 20;

    [SerializeField][Range(MIN_DIFFICULTY, MAX_DIFFICULTY)] protected int difficulty;

    public int Difficulty => difficulty;
}

