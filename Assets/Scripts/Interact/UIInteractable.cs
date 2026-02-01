using UnityEngine;

public class UIInteractable : MonoBehaviour, IInteractable
{
    [SerializeField] GameObject ui;

    public void Close()
    {
        ui.SetActive(false);
    }

    public void Interact()
    {
        ui.SetActive(true);
    }
}
