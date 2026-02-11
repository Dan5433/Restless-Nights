using Extensions;
using UnityEngine;

public class LightSwitch : Interactable
{
    [SerializeField] AudioClip lightSwitchOn;
    [SerializeField] AudioClip lightSwitchOff;
    [SerializeField] AudioClip lightSwitchFail;
    [SerializeField] bool isOn = true;

    public bool IsOn => isOn;

    protected override AudioClip InteractSFX => isOn ? lightSwitchOff : lightSwitchOn;

    protected override bool InteractInternal()
    {
        if (LightManager.IsBreakerDisabled(this))
        {
            Debug.Log("Breaker disabled!", this);
            audioSource.PlayWithRandomPitch(lightSwitchFail);
            return false;
        }

        isOn = !isOn;

        LightManager.LightSwitchStateUpdate();
        UpdateSprite();

        return true;
    }

    public void TurnOff()
    {
        PlayInteractSFX();

        isOn = false;

        LightManager.LightSwitchStateUpdate();
        UpdateSprite();
    }

    void UpdateSprite()
    {
        if (isOn)
            spriteRenderer.sprite = LightManager.Instance.LightSwitchOn;
        else
            spriteRenderer.sprite = LightManager.Instance.LightSwitchOff;
    }
}