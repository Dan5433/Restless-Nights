using TMPro;
using UnityEngine;

[RequireComponent(typeof(TMP_Text))]
public class DisplayVersion : MonoBehaviour
{
    void Start()
    {
        GetComponent<TMP_Text>().text = "v" + Application.version;
    }
}
