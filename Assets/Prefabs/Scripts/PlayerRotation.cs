using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerRotation : MonoBehaviour
{
    [Header("Rotation Settings")]
    public float rotationSpeed = 100f; // adjustable in Inspector
    public GameObject ToRotate;

    public void OnRotate(InputAction.CallbackContext context)
    {
        Vector2 rotationInput = context.ReadValue<Vector2>();
        Debug.Log(rotationInput);

        // Rotate only horizontally (around Y axis)
        float yaw = rotationInput.x * rotationSpeed * Time.deltaTime;
        ToRotate.transform.Rotate(0f, yaw, 0f);
    }
}
