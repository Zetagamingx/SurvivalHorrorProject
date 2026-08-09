using UnityEngine;

public class PlayerActionManager : MonoBehaviour
{
    [SerializeField] private PlayerMovementController playerMovementController;
    [SerializeField] private PlayerCameraController playerCameraController;
    [SerializeField] private PlayerInteraction playerInteractionController;

    public void DisableActions()
    {
        playerMovementController.moveAction.action.Disable();
        playerCameraController.lookAction.action.Disable();
        playerInteractionController.interactAction.action.Disable();
    }

    public void EnableActions()
    {
        playerMovementController.moveAction.action.Enable();
        playerCameraController.lookAction.action.Enable();
        playerInteractionController.interactAction.action.Enable();
    }
}
