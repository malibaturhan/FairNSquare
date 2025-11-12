using UnityCommunity.UnitySingleton;
using UnityEngine;

public class PlayerUpgradeManager : PersistentMonoSingleton<PlayerUpgradeManager>
{

    public void ApplyUpgrade(UpgradeSO upgrade)
    {
        switch (upgrade.upgradeType)
        {
            case UpgradeTypes.Health:
                break;
            case UpgradeTypes.HealInterval:
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
