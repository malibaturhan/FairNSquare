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
    [SerializeField] private int health = 40;

    void Start()
    {
        SetHealthText();
    }

    public void TakeDamage(int amount)
    {
        health -= amount;
        SetHealthText();
        if(health <= 0 )
        {
            health = 0;
            Die();
        }
    }

    private void SetHealthText()
    {
        healthText.text = health.ToString();
    }

    private void Die()
    {
        GameStateManager.Instance.SetGameState(GameState.GameOver);
    }
}
