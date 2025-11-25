using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHPRestart : MonoBehaviour
{
    [Tooltip("Delay before reloading the scene after HP reaches 0.")]
    public float reloadDelay = 0.5f;

    private bool isReloading = false; // prevents multiple reloads

    void Update()
    {
        var gm = GameManager.Instance;

        // Safety: only continue if GameManager exists and not already reloading
        if (gm == null || isReloading) return;

        // Check death condition
        if (gm.playerHP <= 0f  || gm.player2HP <= 0f)
        {
            isReloading = true; // lock out repeated reloads

            // Reset HP before reload to avoid weird carryover (optional)
            gm.playerHP = gm.maxHP;
            gm.player2HP = gm.player2maxHP;

            // Start delayed reload to avoid issues with physics or animation finishing
            Invoke(nameof(ReloadScene), reloadDelay);
        }
    }

    private void ReloadScene()
    {
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }
}