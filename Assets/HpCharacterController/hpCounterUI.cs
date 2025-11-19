using UnityEngine;
using UnityEngine.UI;

public class hpCounterUI : MonoBehaviour
{
    private Image hpBar;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        hpBar = GetComponent<Image>(); 
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance == null || hpBar == null) return;
        float currentHP = GameManager.Instance.playerHP / GameManager.Instance.maxHP;
        hpBar.fillAmount = Mathf.Clamp01(currentHP);
    }
}
