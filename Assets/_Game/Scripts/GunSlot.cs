using System;
using UnityEngine;

[RequireComponent(typeof(PolygonCollider2D))]
public class GunSlot : MonoBehaviour
{

    public enum GunSlotStates
    {
        Empty,
        Active
    }

    public enum GunSlotOrientation
    {
        TOP,
        BOTTOM,
        LEFT,
        RIGHT
    }

    [Header(">>> Runtime Gun Parameters (DON'T CHANGE BY HAND) <<<")]
    private Sprite gunSprite;
    private float currentFireRate;
    private float currentDetectionRange;
    private string weaponName;
    private float currentDamage;
    private float currentCriticalChance;
    private float currentCriticalBonus;
    private int currentGoThroughEnemiesCount;
    private GameObject bulletPrefab;
    private Color currentColor;
    private bool isUnlocked;
    private int currentUnlockCost;
    private float timeSinceFiring = 0f;


    [Header("***Elements***")]
    [SerializeField] public GunSO GunInSlot;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private PolygonCollider2D polygonCollider;

    [Header("***Settings")]
    [SerializeField] private Sprite emptySlotSprite;
    private GunSlotStates currentState;
    [SerializeField] private GunSlotOrientation orientation;
    private Vector2 direction;


    void Start()
    {

        polygonCollider.isTrigger = true;
        SetOrientation();
        CheckSlot();
    }

    private void SetOrientation()
    {

        switch (orientation)
        {
            case GunSlotOrientation.TOP:
                direction = transform.TransformDirection(Vector2.up);
                spriteRenderer.transform.localRotation = Quaternion.Euler(0, 0, 90);
                break;
            case GunSlotOrientation.BOTTOM:
                direction = transform.TransformDirection(Vector2.down);
                spriteRenderer.transform.localRotation = Quaternion.Euler(0, 0, -90);
                break;
            case GunSlotOrientation.RIGHT:
                direction = transform.TransformDirection(Vector2.right);
                spriteRenderer.transform.localRotation = Quaternion.Euler(0, 0, 0);
                break;
            case GunSlotOrientation.LEFT:
                direction = transform.TransformDirection(Vector2.left);
                spriteRenderer.transform.localRotation = Quaternion.Euler(0, 0, -180);
                break;

        }
    }


    void Update()
    {
        SetOrientation();
        timeSinceFiring -= Time.deltaTime;
        if (GunInSlot != null && currentState == GunSlotStates.Active)
        {
            if (timeSinceFiring <= 0f)
            {
                Fire();
                timeSinceFiring = 1f / currentFireRate;
                Debug.LogWarning($"timesincefiring set to {timeSinceFiring}");
            }
        }
    }


    private void Fire()
    {
        GunInSlot.gunBehaviour.Fire(this, direction);

    }

    private void OnSlotStateChanged(GunSlotStates newGunSlotState)
    {
        currentState = newGunSlotState;
        switch (newGunSlotState)
        {
            case GunSlotStates.Empty:
                break;
            case GunSlotStates.Active:

                break;
        }
    }
    private void CheckSlot()
    {
        if (GunInSlot != null)
        {
            SetGun();
        }
    }

    private void SetGun()
    {
        InitializeFromSO();
        spriteRenderer.sprite = GunInSlot.gunSprite;
        OnSlotStateChanged(GunSlotStates.Active);
    }


    private void InitializeFromSO()
    {
        currentFireRate = GunInSlot.fireRate;
        gunSprite = GunInSlot.gunSprite;
        currentDetectionRange = GunInSlot.detectionRange;
        weaponName = GunInSlot.weaponName;
        currentDamage = GunInSlot.damage;
        currentCriticalChance = GunInSlot.criticalChance;
        currentCriticalBonus = GunInSlot.criticalDamageAdditionPercentage;
        currentGoThroughEnemiesCount = GunInSlot.goThroughEnemiesCount;
        bulletPrefab = GunInSlot.bulletPrefab;
        currentColor = GunInSlot.color;
        isUnlocked = GunInSlot.isUnlocked;
        currentUnlockCost = GunInSlot.unlockCost;
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        //Gizmos.DrawSphere(transform.position, 0.2f);
        Gizmos.DrawLine(transform.position, direction);
    }
}
