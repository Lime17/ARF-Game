using UnityEngine;
using UnityEngine.InputSystem;

public class InstantiateBaseball : MonoBehaviour
{
    public GameObject Ball;
    public GameObject BallspawnPoint;
    public float upwardForce = 5f;

     // Player-owned ball reference
    private Baseball activeBall;

   public void OnBallThrow(InputAction.CallbackContext context){
    if (!context.performed)
    return;

      // If THIS player already has a ball, do nothing
        if (activeBall != null)
            return;
        
        //Spawn ball
        GameObject ball = Instantiate(Ball, BallspawnPoint.transform.position, Quaternion.identity);

    //Apply upward force
    Rigidbody rb = ball.GetComponent<Rigidbody>();
    if (rb != null)
    {
        rb.AddForce(Vector3.up * upwardForce, ForceMode.Impulse);  // Adjust force amount in Inspector eventually
    }

       // Register ownership
        activeBall = ball.GetComponent<Baseball>();
        if (activeBall != null)
        {
            activeBall.ownerSpawner = this;
        }

          // Called by Baseball when it is destroyed
   }

    public void ClearBallReference(Baseball ball)
    {
        if (activeBall == ball)
        {
            activeBall = null;
        }
    
    }
   
}

