using Extensions;
using UnityEngine;

public class LightSwitch : Interactable
{
    [SerializeField] bool isOn = true;

    public bool IsOn => isOn;

    public override AudioClip InteractSFX => isOn ? AudioManager.Instance.LightSwitchOff : AudioManager.Instance.LightSwitchOn;

    protected override bool InteractInternal()
    {
        if (LightManager.IsBreakerDisabled(this))
        {
            Debug.Log("Breaker disabled!", this);
            audioSource.PlayOneShotWithRandomPitch(AudioManager.Instance.LightSwitchFail);
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