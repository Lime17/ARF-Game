using UnityEngine;

public class Baseball : MonoBehaviour
{
    [Header("Lifetime Settings")]
    public float baseballTimer = 3.0f;   // Time until ball destroys itself
    private float timer;                 // internal countdown

    [Header("Damage Settings")]
    public float damageIncreasePerBounce = 5f; // How much extra damage per hit
    private hitPlayer hitScript;               // reference to existing damage script

    void Start()
    {
        timer = baseballTimer;

        // Get the hitPlayer component attached to this baseball
        hitScript = GetComponent<hitPlayer>();

        if (hitScript == null)
        {
            Debug.LogWarning("Baseball has no hitPlayer component!");
        }
    }

    void Update()
    {
        // Countdown timer
        timer -= Time.deltaTime;

        if (timer <= 0f)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // If hit by bat—reset timer AND increase damage
        if (other.CompareTag("Bat"))
        {
            // Reset timer to original value
            timer = baseballTimer;

            // Increase damage
            if (hitScript != null)
            {
                hitScript.damage += damageIncreasePerBounce;
            }
        }
    }
}
