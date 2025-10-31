using System;
using UnityEngine;

public class GunSlot : MonoBehaviour
{

    public enum GunSlotStates
    {
        Empty,
        Active
    }

    [Header("***Elements***")]
    [SerializeField] private GunSO gunInSlot;
    [SerializeField] private SpriteRenderer spriteRenderer;

    [Header("***Settings")]
    [SerializeField] private Sprite emptySlotSprite;
    private GunSlotStates currentState;
    void Start()
    {
        CheckSlot();
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

    void Update()
    {
        if (gunInSlot != null && currentState == GunSlotStates.Active) 
        {
            Fire();
        }
    }

    private void Fire()
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

    //private void OnDrawGizmos()
    //{
    //    Gizmos.color = Color.yellow;
    //    Gizmos.DrawSphere(transform.position, 0.2f);
    //}
}
