using Extensions;
using UnityEngine;

public class UIInteractable : Interactable
{
    [SerializeField] AudioClip interactSFX;
    [SerializeField] AudioClip closeUiSFX;
    [SerializeField] GameObject ui;

    public override AudioClip InteractSFX => interactSFX;
    public AudioClip CloseUiSFX => closeUiSFX;

    public void Close()
    {
        ui.SetActive(false);
        PlayCloseSFX();
    }

    public void PlayCloseSFX()
    {
        audioSource.PlayOneShotWithRandomPitch(closeUiSFX);
    }

    protected override bool InteractInternal()
    {
        ui.SetActive(true);
        return true;
    }
}
