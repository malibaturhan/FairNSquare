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
    [SerializeField] private int currentPlayerLevel;
    [SerializeField] private GunSlot leftGunSlot;
    [SerializeField] private GunSlot topGunSlot;
    [SerializeField] private GunSlot rightGunSlot;
    [SerializeField] private GunSlot bottomGunSlot;
    private GunSlot[] GunSlots = new GunSlot[4];
    private int activeGunSlotIndex = 0;
    private int lastFilledGunSlotIndex = 0;

    [Header("***Settings***")]
    [SerializeField] private float incrementPercentageForNextLevel = 0.1f;
    [SerializeField] private float initialXPForLevelUp = 100f;
    [SerializeField] private float neededXPForLevelDown; // to calculate xp bar fill
    [SerializeField] private float neededXPForLevelUp;

    public int Level => currentPlayerLevel;

    void Start()
    {
        GunSlots = new GunSlot[] { leftGunSlot, topGunSlot, rightGunSlot, bottomGunSlot };
        neededXPForLevelDown = 0;
        neededXPForLevelUp = initialXPForLevelUp;
        UpdateUI();
        InitializeGuns();
    }

    private void InitializeGuns()
    {
        ChangeActiveSlot(false);
        ChangeActiveSlot(true);
    }

    public void PutGunOnSlot(GunSO newGun)
    {
        if (lastFilledGunSlotIndex + 1 < GunSlots.Length)
        {
            GunSlots[lastFilledGunSlotIndex + 1].GunInSlot = newGun;
        }
    }

    public void ChangeActiveSlot(bool toRight)
    {
        foreach (var gunSlot in GunSlots)
        {

            Debug.LogWarning($"Currently looping {gunSlot.name}");
            if (gunSlot.CurrentState == GunSlot.GunSlotState.Active)
            {
                gunSlot.ChangeGunState(GunSlot.GunSlotState.Passive);
            }
        }
        activeGunSlotIndex = toRight? (activeGunSlotIndex + 1 + GunSlots.Length) % GunSlots.Length : (activeGunSlotIndex - 1 + GunSlots.Length) % GunSlots.Length;

        GunSlots[activeGunSlotIndex].ChangeGunState(GunSlot.GunSlotState.Active);
    }

    internal void IncreaseXP(float xpGain)
    {
        currentPlayerXP += xpGain;
        if (currentPlayerXP > neededXPForLevelUp)
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
        //TRIGGER LEVEL UP FROM STATE
        GameStateManager.Instance.SetGameState(GameState.LevelUp);
    }

    private void UpdateUI()
    {
        levelText.text = "lvl " + currentPlayerLevel;
        float interval = neededXPForLevelUp - neededXPForLevelDown;
        float increment = currentPlayerXP - neededXPForLevelDown;
        float fillAmount = increment / interval;
        xpFillImage.fillAmount = fillAmount;
    }


}
