using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;  // Import TextMeshPro

public class Enemy : MonoBehaviour, IDamageable
{
    private int health = 100;
    [SerializeField] private TMP_Text healthText; // Reference ke TextMeshPro UI

    private void Start()
    {
        UpdateHealthText();
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        Debug.Log($"{gameObject.name} took {damage} damage. Health remaining: {health}");
        UpdateHealthText();
        GameMaster.instance.AddScore(20);
        if (health <= 0)
        {
            GameMaster.instance.WIN(); // Call the win method from GameMaster
            gameObject.SetActive(false); // Deactivate the enemy when health is 0
        }
    }
    public void Reset()
    {
        gameObject.SetActive(true); // Reactivate the enemy
        health = 100;
        UpdateHealthText();
    }

    private void UpdateHealthText()
    {
        if (healthText != null)
        {
            healthText.text = $"Health: {health}";
        }
    }
}