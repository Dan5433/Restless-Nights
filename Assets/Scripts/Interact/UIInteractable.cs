using UnityEngine;

public class UIInteractable : Interactable
{
    [SerializeField] AudioClip interactSFX;
    [SerializeField] GameObject ui;

    protected override AudioClip InteractSFX => interactSFX;

    public void Close()
    {
        ui.SetActive(false);
    }

    protected override void InteractInternal()
    {
        ui.SetActive(true);
    }
}
