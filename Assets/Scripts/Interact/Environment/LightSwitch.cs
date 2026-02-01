using UnityEngine;

public class LightSwitch : MonoBehaviour, IInteractable
{
    [SerializeField] bool isOn = true;

    public bool IsOn => isOn;

    public void Interact()
    {
        isOn = !isOn;

        LightManager.LightSwitchStateUpdate();
    }
}
