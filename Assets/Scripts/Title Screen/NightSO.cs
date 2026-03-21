using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Night")]
public class NightSO : ScriptableObject
{
    [SerializeField] DialogManager.Message[] nightStartMessages, firstTaskMessages;
    [SerializeField] Difficulty difficulty;

    public DialogManager.Message[] NightStartMessages => nightStartMessages;
    public DialogManager.Message[] FirstTaskMessages => firstTaskMessages;

    public Difficulty NightDifficulty { get { return difficulty; } set { difficulty = value; } }

    [Serializable]
    public struct Difficulty
    {
        public int panic, doorways, tasks, monster;
    }
}
