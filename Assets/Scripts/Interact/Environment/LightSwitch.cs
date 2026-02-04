using UnityEngine;

public class LightSwitch : MonoBehaviour, IInteractable
{
    [SerializeField] bool isOn = true;
    SpriteRenderer spriteRenderer;

    public bool IsOn => isOn;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void Interact()
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