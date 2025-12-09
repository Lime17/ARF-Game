using UnityEngine;

public class TurnOn : MonoBehaviour
{
    public GameObject ToTurnOn;
    public GameObject spawnPoint;
   public void OnObjectEnable()
    {
        Instantiate(ToTurnOn, spawnPoint.transform.position, spawnPoint.transform.rotation);
    }
}
