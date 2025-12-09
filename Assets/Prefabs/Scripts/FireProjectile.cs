using UnityEngine;
using UnityEngine.InputSystem;

public class FireProjectile : MonoBehaviour
{
    // Only GameObjects with a Rigidbody can be assigned as the projectile.
    public Rigidbody projectile;
    // Speed of the projectile when fired.
    public float speed = 4;
    // This method checks for input and fires a projectile if the attack action is pressed.
    public void OnProjectileFire(InputAction.CallbackContext context)
    {
        // Instantiate a projectile at the player's position and set its velocity.
            Rigidbody p = Instantiate(projectile, transform.position, transform.rotation);
            p.linearVelocity = transform.forward * speed;
        
    }
}