using TMPro;
using UnityEngine;

[CreateAssetMenu(menuName = "Validator/Difficulty")]
public class DifficultyValidator : TMP_InputValidator
{
    public override char Validate(ref string text, ref int pos, char ch)
    {
        if (!char.IsDigit(ch) || text == DifficultySingleton<TitleScreen>.MIN_DIFFICULTY.ToString())
            return char.MinValue;

        int difficulty = int.Parse(text + ch);
        if (difficulty > DifficultySingleton<TitleScreen>.MAX_DIFFICULTY)
            return char.MinValue;

        text += ch;
        pos++;
        return ch;
    }
}
