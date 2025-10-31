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

    [Header("***Elements***")]
    [SerializeField] private GunSO gunInSlot;
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
                break;
            case GunSlotOrientation.BOTTOM:
                direction = transform.TransformDirection(Vector2.down);
                break;
            case GunSlotOrientation.RIGHT:
                direction = transform.TransformDirection(Vector2.right);
                break;
            case GunSlotOrientation.LEFT:
                direction = transform.TransformDirection(Vector2.left);
                break;
        
    }
}


    void Update()
    {
        SetOrientation();

        if (gunInSlot != null && currentState == GunSlotStates.Active) 
        {
            //Fire();
        }
    }

    //private void TryFire()
    //{
    //    Transform[] enemies = GetEnemiesInSight();
    //}

    private Transform[] GetEnemiesInSight()
    {
        throw new NotImplementedException();
    }

    private void Fire(Vector2 direction)
    {
        Debug.Log("Firing");
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
        if (gunInSlot != null) 
        {
            SetGun();
        }
    }

    private void SetGun()
    {
        spriteRenderer.sprite = gunInSlot.gunSprite;
        OnSlotStateChanged(GunSlotStates.Active);
    }

    private void SetDetectionColliderLength()
    {

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        //Gizmos.DrawSphere(transform.position, 0.2f);
        Gizmos.DrawLine(transform.position, direction);
    }
}
