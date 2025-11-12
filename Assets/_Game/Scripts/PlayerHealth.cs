using System;
using UnityEngine;
using UnityEngine.UI;
using UnityCommunity.UnitySingleton;
using TMPro;

public class PlayerHealth : PersistentMonoSingleton<PlayerHealth>
{
    [Header("***Elements***")]
    [SerializeField] public TextMeshProUGUI healthText;

    [Header("***Settings***")]
    [SerializeField] private int maxHealth = 40;
    [SerializeField] private float timeBetweenHeal = 5;
    private int currentHealth;

    [Header("***Runtime Elements***")]
    private float timePassedSinceHeal = 0;

    void Start()
    {
        Reset();
    }

    public void Reset()
    {
        currentHealth = maxHealth;
        SetHealthText();
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        SetHealthText();
        if(currentHealth <= 0 )
        {
            currentHealth = 0;
            Die();
        }
    }

    private void FixedUpdate()
    {
        timePassedSinceHeal += Time.fixedDeltaTime;
        if(Time.fixedDeltaTime < timeBetweenHeal)
        {
            if(currentHealth < maxHealth)
            {
                currentHealth++;
                timePassedSinceHeal = 0f;
            }
        }
        SetHealthText();
    }

    private void SetHealthText()
    {
        healthText.text = currentHealth.ToString();
    }

    private void Die()
    {
        GameStateManager.Instance.SetGameState(GameState.GameOver);
    }
}
