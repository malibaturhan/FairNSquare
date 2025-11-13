using UnityEngine;

[CreateAssetMenu(fileName = "NewUpgrade", menuName = "Upgrade/UpgradeSO")]
public class UpgradeSO : ScriptableObject
{
    [SerializeField, Range(0f,0.9f)] public float rarity;
    [SerializeField] public float effectAmount;
    [SerializeField] public UpgradeTypes upgradeType;
    [SerializeField] public string Name;
    [SerializeField] public string Description;
}
