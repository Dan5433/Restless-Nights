using EditorAttributes;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    [SerializeField] float reach;
    [SerializeField] Transform origin;
    [SerializeField] string interactLayer = "Interact";
    [SerializeField] PlayerMovement movement;

    [SerializeField, DisableInEditMode, DisableInPlayMode] UIInteractable activeUi;
    [SerializeField, DisableInEditMode, DisableInPlayMode] Interactable highlightedInteractable;

    private void Update()
    {
        if (CanDisableUI())
        {
            activeUi.Close();
            movement.Locked = false;
            activeUi = null;
        }

        if (movement.Locked)
            return;

        if (activeUi)
            return;

        int interactMask = LayerMask.GetMask(interactLayer);
        RaycastHit2D hit = Physics2D.Raycast(origin.position, origin.up, reach, interactMask);

        Debug.DrawRay(origin.position, origin.up * reach, Color.green);

        if (!hit)
        {
            if (highlightedInteractable)
            {
                highlightedInteractable.RaycastExit();
                highlightedInteractable = null;
            }

            return;
        }

        if (!hit.transform.TryGetComponent(out Interactable interactable))
            return;

        interactable.RaycastEnter();
        highlightedInteractable = interactable;

        if (!Input.GetKeyDown(KeyCode.Mouse1))
            return;

        interactable.Interact();

        if (!hit.transform.TryGetComponent(out UIInteractable uiInteractable))
            return;

        activeUi = uiInteractable;
        movement.Locked = true;
    }

    bool CanDisableUI()
    {
        if (activeUi == null)
            return false;

        if (Input.GetKeyDown(KeyCode.Escape))
            return true;

        if (Input.GetMouseButtonDown(1))
            return true;

        return false;
    }
}
