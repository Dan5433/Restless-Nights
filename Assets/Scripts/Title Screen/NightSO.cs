using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Night")]
public class NightSO : ScriptableObject
{
    [SerializeField] DialogManager.Message[] nightStartMessages, firstTaskMessages;
    [SerializeField] Difficulty difficulty;

    [Serializable]
    struct Difficulty
    {
        public int panic, doorways, tasks, monster;
    }
}
