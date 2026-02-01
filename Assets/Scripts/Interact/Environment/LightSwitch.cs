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
        isOn = !isOn;

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