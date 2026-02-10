using UnityEngine;

public class LightSwitch : Interactable
{
    [SerializeField] AudioClip lightSwitchOn;
    [SerializeField] AudioClip lightSwitchOff;
    [SerializeField] bool isOn = true;

    public bool IsOn => isOn;

    protected override AudioClip InteractSFX => isOn ? lightSwitchOff : lightSwitchOn;

    protected override void InteractInternal()
    {
        if (LightManager.IsBreakerDisabled(this))
        {
            Debug.Log("Breaker disabled!", this);
            //play electricity sound effect
            return;
        }

        isOn = !isOn;

        LightManager.LightSwitchStateUpdate();
        UpdateSprite();
    }

    public void TurnOff()
    {
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