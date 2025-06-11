using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;

public class SimplePlayerUI : MonoBehaviour
{
    [Header("Stats")]
    public float maxHealth = 100f;
    public float currentHealth;

    public float maxExp = 100f;
    public float currentExp;

    [Header("UI Elements")]
    public Image healthBar;
    public Image expBar;
    public TextMeshProUGUI coinText;

    [Header("Inventory")]
    public int coinAmount = 0;

    void Start()
    {
        currentHealth = maxHealth;
        currentExp = 0f;
        UpdateUI();
    }

    public void AddHealth(float amount)
    {
        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
        if(currentHealth <= 0)
        {
            GameMaster.instance.Lose();
            Debug.Log("Player has died.");
        }
        UpdateUI();
    }

    public void AddExp(float amount)
    {
        currentExp = Mathf.Clamp(currentExp + amount, 0, maxExp);
        UpdateUI();
    }

    public void AddCoin(int amount)
    {
        coinAmount += amount;
        UpdateUI();
    }

    public void UpdateUI()
    {
        if (healthBar != null)
            healthBar.fillAmount = currentHealth / maxHealth;

        if (expBar != null)
            expBar.fillAmount = currentExp / maxExp;

        if (coinText != null)
            coinText.text = coinAmount.ToString();
    }
}
