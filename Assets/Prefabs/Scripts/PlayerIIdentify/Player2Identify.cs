using UnityEngine;

public class Player2Identify : MonoBehaviour
{   public Player1Identify playerID;
    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.collider.CompareTag("Attack"))
        {
            playerID.playerNumber = 2;
        }
}
}