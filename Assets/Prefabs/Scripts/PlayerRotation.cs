using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerRotation : MonoBehaviour
{
    [Header("Rotation Settings")]
    public float rotationSpeed = 100f; // adjustable in Inspector

    public void OnRotate(InputAction.CallbackContext context)
    {
        Vector2 rotationInput = context.ReadValue<Vector2>();

        // Rotate only horizontally (around Y axis)
        float yaw = rotationInput.x * rotationSpeed * Time.deltaTime;
        transform.Rotate(0f, yaw, 0f);
    }
}
