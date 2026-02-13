using Extensions;
using UnityEngine;

public class UIInteractable : Interactable
{
    [SerializeField] AudioClip interactSFX;
    [SerializeField] AudioClip closeUiSFX;
    [SerializeField] GameObject ui;

    protected override AudioClip InteractSFX => interactSFX;

    public void Close()
    {
        ui.SetActive(false);
        audioSource.PlayOneShotWithRandomPitch(closeUiSFX);
    }

    protected override bool InteractInternal()
    {
        ui.SetActive(true);
        return true;
    }
}
