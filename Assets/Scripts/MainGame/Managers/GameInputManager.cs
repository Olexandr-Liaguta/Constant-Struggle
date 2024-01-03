using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInputManager : MonoBehaviour
{

    public static GameInputManager Instance { get; private set; }

    private InputActions inputActions;

    [SerializeField] private PlayerInteraction playerInteraction;



    private void Awake()
    {
        Instance = this;

        inputActions = new InputActions();
        inputActions.Player.Enable();

        inputActions.Player.Inventory.performed += Inventory_performed;
        inputActions.Player.QuickSave.performed += QuickSave_performed;
        inputActions.Player.Escape.performed += Escape_performed;
        inputActions.Player.Interaction.performed += Interaction_performed;
    }

    private void Interaction_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        playerInteraction.HandleInteract();
    }

    private void Escape_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        if (PlayerInventoryManager.Instance.IsActive())
        {
            PlayerInventoryManager.Instance.HideInventory();
        }
        else
        {
            if (PauseGameManager.Instance.IsGamePaused())
            {

                PauseGameManager.Instance.ResumeGame();
            }
            else
            {
                PauseGameManager.Instance.PauseGame();

            }

        }
    }

    private void QuickSave_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        SaveManager.Instance.Save();
    }

    private void Inventory_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        PlayerInventoryManager.Instance.ToogleInventory();
    }

    public Vector2 GetMovement()
    {
        Vector2 movement = inputActions.Player.Movement.ReadValue<Vector2>();

        return movement;
    }

    public void DisablePlayerActions()
    {
        inputActions.Player.Disable();
    }
}
