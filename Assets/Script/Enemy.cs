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

        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void UpdateHealthText()
    {
        if (healthText != null)
        {
            healthText.text = $"Health: {health}";
        }
    }
}