using System;
using UnityEngine;

public class GunSlot : MonoBehaviour
{

    public enum GunSlotState
    {
        Empty,
        Passive,
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
    [SerializeField] private Transform bulletContainer;
    [SerializeField] private LineRenderer aimSightLineRenderer;

    [Header("***Settings")]
    [SerializeField] private Sprite emptySlotSprite;
    private GunSlotState currentState;
    [SerializeField] private GunSlotOrientation orientation;
    private Vector2 direction;
    [SerializeField] private float sightDistance;

    public GunSlotState CurrentState => currentState;

    void Start()
    {
        InitializeLineRenderer();
        SetOrientation();
        CheckSlot();
    }

    private void InitializeLineRenderer()
    {
        if (GunInSlot != null)
        {
            aimSightLineRenderer.colorGradient = GunInSlot.aimSightLineRendererGradient;

        }
    }

    private void SetOrientation()
    {

        switch (orientation)
        {
            case GunSlotOrientation.TOP:
                direction = transform.TransformDirection(Vector2.up);
                aimSightLineRenderer.SetPosition(1, direction * sightDistance);
                spriteRenderer.transform.localRotation = Quaternion.Euler(0, 0, 90);
                break;
            case GunSlotOrientation.BOTTOM:
                direction = transform.TransformDirection(Vector2.down);
                aimSightLineRenderer.SetPosition(1, direction * sightDistance);
                spriteRenderer.transform.localRotation = Quaternion.Euler(0, 0, -90);
                break;
            case GunSlotOrientation.RIGHT:
                direction = transform.TransformDirection(Vector2.right);
                aimSightLineRenderer.SetPosition(1, direction * sightDistance);
                spriteRenderer.transform.localRotation = Quaternion.Euler(0, 0, 0);
                break;
            case GunSlotOrientation.LEFT:
                direction = transform.TransformDirection(Vector2.left);
                aimSightLineRenderer.SetPosition(1, direction * sightDistance);
                spriteRenderer.transform.localRotation = Quaternion.Euler(0, 0, -180);
                break;

        }
    }

    public void ChangeGunState(GunSlotState newState)
    {
        if (newState == GunSlotState.Active)
        {
            aimSightLineRenderer.enabled = true;
        }
        else
        {
            aimSightLineRenderer.enabled = false;
        }
        currentState = newState;
    }


    void Update()
    {
        SetOrientation();
        timeSinceFiring -= Time.deltaTime;
        if (GunInSlot != null && currentState == GunSlotState.Active)
        {
            if (timeSinceFiring <= 0f)
            {
                if (currentState == GunSlotState.Active)
                {
                    Fire();
                }
                timeSinceFiring = 1f / currentFireRate;
                //Debug.LogWarning($"timesincefiring set to {timeSinceFiring}");
            }
        }
    }


    private void Fire()
    {
        GunInSlot.gunBehaviour.Fire(this, direction, bulletContainer);
    }

    private void OnSlotStateChanged(GunSlotState newGunSlotState)
    {
        currentState = newGunSlotState;
        switch (newGunSlotState)
        {
            case GunSlotState.Empty:
                break;
            case GunSlotState.Active:

                break;
        }
    }
    private void CheckSlot()
    {
        if (GunInSlot != null)
        {
            SetGun();
            aimSightLineRenderer.enabled = true;
        }
        else
        {
            aimSightLineRenderer.enabled = false;
        }
    }

    private void SetGun()
    {
        InitializeFromSO();
        spriteRenderer.sprite = GunInSlot.gunSprite;
        OnSlotStateChanged(GunSlotState.Active);
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
        currentColor = GunInSlot.gunColorTint;
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
