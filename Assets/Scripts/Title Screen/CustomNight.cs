using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CustomNight : MonoBehaviour
{
    [SerializeField] NightSO customNight;
    [SerializeField] GameObject customNightSelection;
    [SerializeField] TMP_InputField panic, doorways, tasks, monster;

    public void OnClickCustomNight()
    {
        if (customNightSelection.activeInHierarchy)
            PlayCustomNight();
        else
            GetComponentInChildren<TMP_Text>().text += " > Play";
    }

    void PlayCustomNight()
    {
        NightSO night = Instantiate(customNight);
        NightSO.Difficulty difficulty = new()
        {
            panic = ParseDifficultyInput(panic.text),
            doorways = ParseDifficultyInput(doorways.text),
            tasks = ParseDifficultyInput(tasks.text),
            monster = ParseDifficultyInput(monster.text),
        };
        night.NightDifficulty = difficulty;
        night.name = $"{customNight.name} ({difficulty.panic}, {difficulty.doorways}, {difficulty.tasks}, {difficulty.monster})";

        GameManager.LoadNight(night);
        SceneManager.LoadScene(1);
    }

    public void ChangePanicDifficulty(int change)
    {
        ChangeDifficulty(change, panic);
    }

    public void ChangeDoorwaysDifficulty(int change)
    {
        ChangeDifficulty(change, doorways);
    }

    public void ChangeTasksDifficulty(int change)
    {
        ChangeDifficulty(change, tasks);
    }

    public void ChangeMonsterDifficulty(int change)
    {
        ChangeDifficulty(change, monster);
    }

    void ChangeDifficulty(int change, TMP_InputField field)
    {
        int difficulty = ParseDifficultyInput(field.text);
        if (change > 0 && difficulty < DifficultySingleton<CustomNight>.MAX_DIFFICULTY
            || change < 0 && difficulty > DifficultySingleton<CustomNight>.MIN_DIFFICULTY)
        {
            difficulty += change;
            field.text = difficulty.ToString();
        }
    }

    int ParseDifficultyInput(string text)
    {
        return string.IsNullOrWhiteSpace(text) ? 0 : int.Parse(text);
    }
}
