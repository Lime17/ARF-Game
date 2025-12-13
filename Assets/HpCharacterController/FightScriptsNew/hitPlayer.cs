using UnityEngine;
using System.Collections;

public class hitPlayer : MonoBehaviour
{

    //This goes into the thing that hits (melee)
    //How much damage it makes
    public float damage = 10f;
    public float objectDamage = 10f;

    private bool hasDealtDamage = false;

    //How much knockback it causes
    public float force = 10f;

    //How much cooldown it causes
    public float cooldown = 1f;

    private bool canHit = true;

    public int attackerPlayerID = -1;

        void Awake()
        {
            // Look up the hierarchy for an owner
            AttackOwnership owner = GetComponentInParent<AttackOwnership>();

            if (owner != null)
            {
                attackerPlayerID = owner.playerID;
            }
            else
            {
                Debug.LogWarning($"{name} has no AttackOwner in parent hierarchy");
            }
        }

    private void OnTriggerEnter(Collider collider)
    {

        if (canHit)
        {
            if (collider.CompareTag("Player"))
            {
                Debug.Log("I am touching the player");


                 // Get the player's HP controller to read the Player ID
                var hpCollider = collider.GetComponent<PlayerHPCollider>();

                if (hpCollider != null)
        {

                GameManager.Instance.RemoveHP(damage, hpCollider.thisPlayerID, attackerPlayerID);// add player number in 2 player game
        }

                var PlayerKnockback = collider.GetComponent<PlayerKnockback>();

                if(PlayerKnockback != null)
                {
                    //player pos - weapn position
                    Vector3 pushDirection = (collider.transform.position - transform.position).normalized;
                    pushDirection.y = 0;//remove to send people up

                    PlayerKnockback.Knockback(pushDirection, force, cooldown);
                }
            }
            else    {

               if (hasDealtDamage) return; // already dealt damage, ignore
                var breakable = collider.GetComponent<BreakableObjects>();
                if (breakable != null)
            {
                breakable.TakeDamage(objectDamage);
                hasDealtDamage = true; // mark that this object already hit something
            }
            }

        }
    }

}
