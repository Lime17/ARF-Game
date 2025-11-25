using UnityEngine;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(AudioSource))]
public class PlayerHPCollider : MonoBehaviour
{

[Header("Player ID")]
public int thisPlayaerID;

    [Header("Damage / Cooldown")]
    public float hitCooldown = 1f;
    private float lastHitTime = -999f;

    [Header("Knockback")]
    public float knockStrength = 8f;     // how hard the push is
    public float knockDuration = 0.15f;  // how long before we start damping
    public float knockDamp = 10f;        // how quickly it damps to zero

    [Header("Sound Feedback")]
    public AudioClip HpGainSound;
    public AudioClip HpLoseSound;
    private AudioSource audioS;

    private CharacterController cc;

    // runtime knockback state
    private Vector3 knockVel = Vector3.zero;
    private float knockTimeLeft = 0f;

    void Start()
    {
        cc = GetComponent<CharacterController>();
        audioS = GetComponent<AudioSource>();
        audioS.playOnAwake = false;
    }

    void Update()
    {
        // Apply / decay knockback each frame
        float dt = Time.deltaTime;

        if (knockTimeLeft > 0f)
        {
            knockTimeLeft -= dt;
        }
        else
        {
            // smooth decay after the "hold" phase
            knockVel = Vector3.Lerp(knockVel, Vector3.zero, knockDamp * dt);
        }

        // Add external knockback motion (horizontal)
        if (knockVel.sqrMagnitude > 0.0001f)
        {
            Vector3 motion = knockVel * dt;
            // keep the push planar; your gravity/move script handles vertical
            motion.y = 0f;
            cc.Move(motion);
        }
    }

    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        // ---- HP MODIFY ----
        // Respect cooldown
        if (Time.time - lastHitTime < hitCooldown) return;

        // Does the other collider carry an HP modifier?
        var hpMod = hit.collider.GetComponent<colliderHpModify>();
        if (hpMod == null) return;

        lastHitTime = Time.time;

        // Apply delta (damage or heal) - assuming hpChange is signed (- = damage, + = heal)
        if (hpMod.hpChange < 0)
        {
            GameManager.Instance?.RemoveHP(-hpMod.hpChange, thisPlayaerID);
            if (HpLoseSound) audioS.PlayOneShot(HpLoseSound);
        }
        else
        {
            GameManager.Instance?.AddHP(hpMod.hpChange);
            if (HpGainSound) audioS.PlayOneShot(HpGainSound);
        }


        // ---- KNOCKBACK ----
        // Direction away from the object we hit, horizontal
        Vector3 fromOther = transform.position - hit.collider.bounds.ClosestPoint(transform.position);
        fromOther.y = 0f;
        if (fromOther.sqrMagnitude > 0.0001f)
            fromOther.Normalize();
        else
            fromOther = -Vector3.ProjectOnPlane(hit.moveDirection, Vector3.up).normalized; // fallback

        ApplyKnockback(fromOther, knockStrength, knockDuration);
    }

    // Public so other scripts (e.g., explosions) can reuse it
    public void ApplyKnockback(Vector3 worldDir, float strength, float duration)
    {
        worldDir.y = 0f;
        if (worldDir.sqrMagnitude > 0.0001f) worldDir.Normalize();

        knockVel = worldDir * strength;  // instantaneous "impulse-like" velocity
        knockTimeLeft = duration;        // hold full strength for a short burst
    }
}
