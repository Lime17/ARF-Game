using UnityEngine;

public class TurnOff : MonoBehaviour
{
  
   public void OnAnimationEnd()
    {
        Destroy(transform.parent.gameObject);
    }
}
