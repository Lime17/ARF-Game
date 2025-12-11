using UnityEngine;
using UnityEngine.InputSystem;

public class InstantiateBaseball : MonoBehaviour
{
    public GameObject Ball;
    public GameObject BallspawnPoint;
    public float upwardForce = 5f;

   public void OnBallThrow(InputAction.CallbackContext context){
    if (context.performed){
        
        GameObject ball = Instantiate(Ball, BallspawnPoint.transform.position, Quaternion.identity);

    Rigidbody rb = ball.GetComponent<Rigidbody>();
    if (rb != null)
    {
        rb.AddForce(Vector3.up * upwardForce, ForceMode.Impulse);  // Adjust force amount in Inspector eventually
    }
    
        }
   }
}
