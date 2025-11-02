using System;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    [Header("***Settings***")]
    [SerializeField] private int health = 10;
    [SerializeField] private float moveSpeed = 2f;


    [Header("***Elements***")]
    private Vector2 initialPosition;
    void Start()
    {
        initialPosition = transform.position;
    }

    void FixedUpdate()
    {
        MoveTowardsPlayer();
    }

    public virtual void MoveTowardsPlayer()
    {
        Vector2 dir = (PlayerManager.Instance.transform.position - transform.position).normalized;
        transform.position += (Vector3)dir * moveSpeed * Time.deltaTime;

    }

    public virtual void TakeDamage(int damageTook)
    {
        health -= damageTook;
        if (health <= 0)
        {
            Die();
        }
    }

    public virtual void Die()
    {
        transform.SetParent(null);
        Destroy(gameObject);
    }
}
