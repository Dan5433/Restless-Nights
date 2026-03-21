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
            panic = int.Parse(panic.text),
            doorways = int.Parse(doorways.text),
            tasks = int.Parse(tasks.text),
            monster = int.Parse(monster.text),
        };
        night.NightDifficulty = difficulty;
        night.name = $"{customNight.name} ({difficulty.panic}, {difficulty.doorways}, {difficulty.tasks}, {difficulty.monster})";

        GameManager.LoadNight(night);
        SceneManager.LoadScene(1);
    }
}
