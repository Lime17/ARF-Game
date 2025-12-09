using UnityEngine;

public class BreakableObjects : MonoBehaviour
{
    [Header("Object HP")]
    public float maxHP = 50f;   // per-object, editable in Inspector
    private float currentHP;

    [Header("Break Settings")]
    public GameObject breakEffect; // optional particle effect
    public bool destroyOnBreak = true;

   [Header("Visuals")]
    public Material crackedMaterial;  // assign in Inspector
    private bool hasCracked = false;

    void Start()
    {
        currentHP = maxHP;
    }

    public void TakeDamage(float damageAmount)
    {

        // ---- Apply Damage ----
        currentHP -= damageAmount;

        if (currentHP <= (maxHP * 0.5)){
            
            Crack();

        }

        if (currentHP <= 0)
        {
            Break();
        }
    }

    private void Break()
    {
        if (breakEffect != null)
            Instantiate(breakEffect, transform.position, Quaternion.identity);

        // Do sound, debris, or animations here

        if (destroyOnBreak)
            Destroy(gameObject);
    }

    private void Crack()
{
    if (hasCracked) return; // only crack once

    var renderer = GetComponent<Renderer>(); // get the MeshRenderer
    if (renderer != null && crackedMaterial != null)
    {
        renderer.material = crackedMaterial;
        hasCracked = true;
    }
}
}
