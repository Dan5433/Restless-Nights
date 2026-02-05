using UnityEngine;

public class UIInteractable : Interactable
{
    [SerializeField] GameObject ui;

    public void Close()
    {
        ui.SetActive(false);
    }

    public override void Interact()
    {
        ui.SetActive(true);
    }
}
