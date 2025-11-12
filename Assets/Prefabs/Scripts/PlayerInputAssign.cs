using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Users; // handles pairing/unpairing devices
using UnityEngine.Events;            // Optional: UnityEvent to hook UI responses
using System.Collections.Generic;    // used for array-like lists so that line 15 doesn't need to be repeated throughout the script

public class PlayerInputAssign : MonoBehaviour
{
    public PlayerInput player1Input;
    public PlayerInput player2Input; // Determines which PlayerInput components to use. Drag the PlayerInput of the players that you want controls assigned for into these slots
    public InputActionReference joinActionReference; // allows you to drag action from Input System to be referenced by the script

    private InputAction joinAction;

    private bool p1Assigned = false;
    private bool p2Assigned = false; // variables to tell whether or not the player objects have an input device assigned to them
    private readonly List<InputDevice> assignedDevices = new List<InputDevice>(); // keeps track of which devices are already paired/used

    // Called whenever the Join input action is performed (e.g., pressing Start)
    private void OnJoinPerformed(InputAction.CallbackContext context)
    {
        InputDevice device = context.control.device;
        // .device gets the physical InputDevice that owns that control (e.g., a specific controller or the keyboard)

        // If the device is already assigned, ignore it
        if (assignedDevices.Contains(device))
            return;
        // Assign device to Player 1 if not yet assigned
        if (!p1Assigned)
        {
            assignedDevices.Add(device); // Track the device so it doesn't trigger again
            p1Assigned = true;

            // Ensure only this controller can move this player
            // Deactivate PlayerInput so it doesn't receive input from unpaired devices
            player1Input.DeactivateInput();

            // Unpair any devices currently attached to this PlayerInput's user
            if (player1Input.user.valid)
                player1Input.user.UnpairDevices();

            // Pair the new device to this PlayerInput
            InputUser.PerformPairingWithDevice(device, player1Input.user);

            // Switch the control scheme to match the device (Gamepad vs Keyboard&Mouse)
            player1Input.SwitchCurrentControlScheme(device);

            // Activate the PlayerInput component so it starts receiving input from the paired device
            player1Input.ActivateInput();

            Debug.Log($"Player 1 assigned to {device.displayName}");
            return;
        }

        if (!p2Assigned)
        {
            assignedDevices.Add(device);
            p2Assigned = true;

            player2Input.DeactivateInput();
            if (player2Input.user.valid)
                player2Input.user.UnpairDevices();
            InputUser.PerformPairingWithDevice(device, player2Input.user);
            player2Input.SwitchCurrentControlScheme(device);
            player2Input.ActivateInput();

            Debug.Log($"Player 2 assigned to {device.displayName}");
            return;
        }
    }

    private void OnEnable()
    {
        // Ensure PlayerInputs are off before enabling Join action
        player1Input.DeactivateInput();
        player2Input.DeactivateInput();

        if (joinActionReference != null)
        {
            joinAction = joinActionReference.action;   // get the actual InputAction from the reference
            joinAction.performed += OnJoinPerformed;   // subscribe to performed event
            joinAction.Enable();                        // enable the action so it starts listening
        }
    }

    private void OnDisable()
    {
        if (joinAction != null)
        {
            joinAction.performed -= OnJoinPerformed;  // remove the subscription
            joinAction.Disable();                      // stop listening
        }
    }
}