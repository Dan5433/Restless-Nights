using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScreen : MonoBehaviour
{
    [SerializeField] NightSO[] nights;
    [SerializeField] NightSO customNight;
    [SerializeField] GameObject newGameButton, continueGameButton, customNightButton;
    SaveState save;

    const string SAVE_STATE_KEY = "SaveState";

    void Awake()
    {
        // I know that player prefs are not typically for storing save states, but I don't need to store that much
        string savedjson = PlayerPrefs.GetString(SAVE_STATE_KEY);
        if (savedjson != string.Empty)
            save = JsonUtility.FromJson<SaveState>(savedjson);

        if (save.currentNightIndex > 0)
        {
            continueGameButton.GetComponentInChildren<TMP_Text>().text += $" ({nights[save.currentNightIndex].name})";
            continueGameButton.SetActive(true);
        }
        else
            continueGameButton.SetActive(false);

        customNightButton.SetActive(save.isCustomNightUnlocked);
    }

    public void NewGame()
    {
        save.currentNightIndex = 0;
        WriteSaveState();

        GameManager.LoadNight(nights[0]);
        SceneManager.LoadScene(1);
    }

    public void Continue()
    {
        GameManager.LoadNight(nights[save.currentNightIndex]);
        SceneManager.LoadScene(1);
    }

    void WriteSaveState()
    {
        string json = JsonUtility.ToJson(save);
        PlayerPrefs.SetString(SAVE_STATE_KEY, json);
    }

    [Serializable]
    struct SaveState
    {
        public int currentNightIndex;
        public bool isCustomNightUnlocked;
    }
}
