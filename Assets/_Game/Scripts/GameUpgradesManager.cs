using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameUpgradesManager : MonoBehaviour
{

    [Header("Upgrades")]
    [SerializeField] private UpgradeSO[] upgrades;


    [Header("***Elements***")]
    [SerializeField] private TextMeshProUGUI option1Text;
    [SerializeField] private TextMeshProUGUI option2Text;

    [Header("***Selected Upgrades***")]
    private UpgradeSO option1Upgrade;
    private UpgradeSO option2Upgrade;

    private void OnEnable()
    {
        GameStateManager.OnGameStateChanged += GameStateChangedCallback;
    }

    private void OnDisable()
    {
        GameStateManager.OnGameStateChanged -= GameStateChangedCallback;
    }

    public void RefreshUpgrades()
    {
        option1Upgrade = upgrades[Random.Range(0, upgrades.Length)];
        option2Upgrade = upgrades[Random.Range(0, upgrades.Length)];
        if (option1Upgrade == option2Upgrade)
        {
            option2Upgrade = upgrades[Random.Range(0, upgrades.Length)];
        }
        SetUpgradeTexts();
    }

    private void SetUpgradeTexts()
    {
        option1Text.text = option1Upgrade.name;
        option2Text.text = option2Upgrade.name;
    }

    public void Option1ButtonCallback()
    {
        PlayerUpgradeManager.Instance.ApplyUpgrade(option1Upgrade);
    }
    public void Option2ButtonCallback()
    {
        PlayerUpgradeManager.Instance.ApplyUpgrade(option2Upgrade);
    }

    private void GameStateChangedCallback(GameState state)
    {
        switch (state)
        {
            case GameState.MainMenu:
                break;

            case GameState.Play:
                break;

            case GameState.Pause:
                break;

            case GameState.LevelUp:
                RefreshUpgrades();
                break;

            case GameState.GameOver:
                break;

            default:
                Debug.LogWarning($"Unhandled GameState: {state}");
                break;
        }

    }

}
