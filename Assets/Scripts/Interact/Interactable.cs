using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    public abstract void Interact();

    protected virtual bool CanInteract => true;
}
