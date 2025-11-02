using UnityEngine;

[CreateAssetMenu(fileName = "GunSO", menuName = "Scriptable Objects/GunSO")]
public class GunSO : ScriptableObject
{
    public Sprite gunSprite;
    public float fireRate = 1.0f;
    public float detectionRange = 1.0f;
    public string weaponName;
    public float damage;
    public float criticalChance;
    [Tooltip("How many percent a critical damage is powerful than a normal hit")]
    [Range(20f, 100f)]
    public float criticalDamageAdditionPercentage;
    public int goThroughEnemiesCount;
    public GameObject bulletPrefab;
    public Color color;
    public int unlockCost;
    public bool isUnlocked;

    [Header("Behaviour")]
    public GunBehaviour gunBehaviour;
}
