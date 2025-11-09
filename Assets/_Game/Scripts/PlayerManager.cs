using System;
using TMPro;
using UnityCommunity.UnitySingleton;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : PersistentMonoSingleton<PlayerManager>
{
    [Header("***UI Elements***")]
    [SerializeField] private Image xpFillImage;
    [SerializeField] private TextMeshProUGUI levelText;

    [Header("***Elements***")]
    [SerializeField] private float currentPlayerXP;
    [SerializeField] private float currentPlayerLevel;

    [Header("***Settings***")]
    [SerializeField] private float incrementPercentageForNextLevel = 0.1f;
    [SerializeField] private float initialXPForLevelUp = 100f;
    [SerializeField] private float neededXPForLevelDown; // to calculate xp bar fill
    [SerializeField] private float neededXPForLevelUp;
    void Start()
    {
        neededXPForLevelDown = 0;
        neededXPForLevelUp = initialXPForLevelUp;
        UpdateUI();
    }

    void Update()
    {

    }
    internal void IncreaseXP(float xpGain)
    {
        currentPlayerXP += xpGain;
        if (currentPlayerXP > incrementPercentageForNextLevel) 
        {
            LevelUp();
        }
        UpdateUI();
    }

    private void LevelUp()
    {
        currentPlayerLevel++;
        neededXPForLevelDown = neededXPForLevelUp;
        neededXPForLevelUp = neededXPForLevelUp * (1 + incrementPercentageForNextLevel);
        //MenuManager.Instance.ShowUpgradeMenu();
    }

    private void UpdateUI()
    {
        levelText.text = "lvl " + currentPlayerLevel; 
        float interval = neededXPForLevelDown - neededXPForLevelUp;
        float increment = currentPlayerXP - neededXPForLevelDown;
        float fillAmount = increment / interval;
        xpFillImage.fillAmount = fillAmount;
    }


}
