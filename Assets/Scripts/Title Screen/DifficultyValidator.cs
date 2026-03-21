using TMPro;
using UnityEngine;

[CreateAssetMenu(menuName = "Validator/Difficulty")]
public class DifficultyValidator : TMP_InputValidator
{
    public override char Validate(ref string text, ref int pos, char ch)
    {
        if (!char.IsDigit(ch))
            return char.MinValue;

        string newText = text.Insert(pos, ch.ToString());
        int difficulty = int.Parse(newText);
        if (newText != difficulty.ToString())
            return char.MinValue;

        if (difficulty > DifficultySingleton<CustomNight>.MAX_DIFFICULTY)
            return char.MinValue;

        text = newText;
        pos++;
        return ch;
    }
}
