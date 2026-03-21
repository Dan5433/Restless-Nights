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
            panic = string.IsNullOrWhiteSpace(panic.text) ? 0 : int.Parse(panic.text),
            doorways = string.IsNullOrWhiteSpace(doorways.text) ? 0 : int.Parse(doorways.text),
            tasks = string.IsNullOrWhiteSpace(tasks.text) ? 0 : int.Parse(tasks.text),
            monster = string.IsNullOrWhiteSpace(monster.text) ? 0 : int.Parse(monster.text),
        };
        night.NightDifficulty = difficulty;
        night.name = $"{customNight.name} ({difficulty.panic}, {difficulty.doorways}, {difficulty.tasks}, {difficulty.monster})";

        GameManager.LoadNight(night);
        SceneManager.LoadScene(1);
    }
}
