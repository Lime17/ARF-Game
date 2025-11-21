using UnityEngine;

public class Player1Identify : MonoBehaviour
{    public int playerNumber;
    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.collider.CompareTag("Attack"))
        {
            playerNumber = 1;
        }
}
}