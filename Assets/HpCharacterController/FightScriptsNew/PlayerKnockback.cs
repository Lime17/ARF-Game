using UnityEngine;

public class PlayerKnockback : MonoBehaviour
{
    //This goes in the player

    private Vector3 currentImpact = Vector3.zero;

    private CharacterController cc;

    private float nextHitTime = 0f;

    private float mass = 3.0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        cc = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {

        if(currentImpact.magnitude > 0.2f)
        {
            cc.Move(currentImpact * Time.deltaTime);
        }


        currentImpact = Vector3.Lerp(currentImpact, Vector3.zero, 5 * Time.deltaTime);

    }


    public void Knockback(Vector3 direction,float force, float cooldown)
    {
        Debug.Log("direction: " + direction + " force: " + force + " cooldown: " + cooldown);

        //if the time 
        if (Time.time < nextHitTime) return;

        nextHitTime = Time.time + cooldown;

        direction.Normalize();
        currentImpact += direction * (force/mass);

    }


}
