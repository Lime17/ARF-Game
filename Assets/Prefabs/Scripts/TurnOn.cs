using UnityEngine;
using UnityEngine.InputSystem;

public class TurnOn : MonoBehaviour
{
    public GameObject ToTurnOn;
    public GameObject spawnPoint;

    public int attackerID;
   public void OnObjectEnable(InputAction.CallbackContext context){
    if (context.performed){
        GameObject attack =
        Instantiate(ToTurnOn, spawnPoint.transform.position,  spawnPoint.transform.rotation);

        AttackOwnership owner = attack.GetComponent<AttackOwnership>();
        if (owner != null)
        {
            owner.playerID = attackerID; // your player’s ID
        }    
        }
   }
}
   
