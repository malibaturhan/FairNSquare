using UnityCommunity.UnitySingleton;
using UnityEngine;

public class PlayerUpgradeManager : PersistentMonoSingleton<PlayerUpgradeManager>
{

    public void ApplyUpgrade(UpgradeSO upgrade)
    {
        switch (upgrade.upgradeType)
        {
            case UpgradeTypes.Health:
                PlayerHealth.Instance.UpgradeMaxHealth(Mathf.CeilToInt(upgrade.effectAmount));
                break;
            case UpgradeTypes.HealInterval:
                PlayerHealth.Instance.LowerHealInterval(upgrade.effectAmount);
                break;
            case UpgradeTypes.FireRate:

                break;
            case UpgradeTypes.GunDamage:
                break;
            case UpgradeTypes.Luck:
                break;
        }
        Debug.Log("UPGRADE APPLIED");
    }
}
