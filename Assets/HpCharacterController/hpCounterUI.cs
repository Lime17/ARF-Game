using UnityEngine;
using UnityEngine.UI;

public class hpCounterUI : MonoBehaviour
{
    public Image hpBar;

    public Image P2hpBar;

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance == null || hpBar == null) return;
        float currentHP = GameManager.Instance.playerHP / GameManager.Instance.maxHP;
        hpBar.fillAmount = Mathf.Clamp01(currentHP);
        
        if (GameManager.Instance == null || P2hpBar == null) return;
        float P2currentHP = GameManager.Instance.player2HP / GameManager.Instance.player2maxHP;
        P2hpBar.fillAmount = Mathf.Clamp01(P2currentHP);
    }
}
