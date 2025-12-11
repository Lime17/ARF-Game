using UnityEngine;
using UnityEngine.InputSystem;

public class TurnOn : MonoBehaviour
{
    public GameObject ToTurnOn;
    public GameObject spawnPoint;
   public void OnObjectEnable(InputAction.CallbackContext context){
    if (context.performed){
    
        Instantiate(ToTurnOn, spawnPoint.transform.position, Quaternion.identity);
    
        }
   }
}
   
