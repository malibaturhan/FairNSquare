using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using TMPro;

public abstract class Enemy : MonoBehaviour, IDamagable, ISlowable
{
    [Header("***Settings***")]
    [SerializeField] private int health = 10;
    [SerializeField] private int damageToGive = 10;
    [SerializeField] private float delayBetweenHits = 1f;
    [SerializeField] private float movementFactor = 1f;
    private Coroutine damageCoroutine;

    [Header("***Elements***")]
    private Vector2 initialPosition;
    private List<Collider2D> neighbours = new();
    [SerializeField] TextMeshPro damageTookText;

    [Header("+++Movement+++")]
    private ContactFilter2D contactFilter;
    [SerializeField] private float moveSpeed = 2f;
    [SerializeField] private Vector3 seperationForce = Vector3.zero;
    [SerializeField] private float seperationWeight = 1f;
    [SerializeField] private float cohesionWeight = 1f;
    [SerializeField] private float alignmentWeight = 1f;
    [SerializeField] private float detectionRange = 1f;
    [SerializeField] private LayerMask EnemyLayerMask;


    void Start()
    {
        initialPosition = transform.position;
        contactFilter.SetLayerMask(EnemyLayerMask);
    }

    void FixedUpdate()
    {
        Move();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (damageCoroutine == null)
            {
                damageCoroutine = StartCoroutine(DealDamage());
            }
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (damageCoroutine != null)
            {
                StopCoroutine(damageCoroutine);
                damageCoroutine = null;
            }
        }
    }


    private IEnumerator DealDamage()
    {
        while (true)
        {
            Debug.Log("HIT PLAYER");
            PlayerHealth.Instance.TakeDamage(damageToGive);
            yield return new WaitForSeconds(delayBetweenHits);
        }
    }

  
    public void Move()
    {
        seperationForce = Vector3.zero; // to prevent accumulation
        GetNeighbours();
        //Debug.Log($"{gameObject.name} HAS {neighbours.Count} NEIGHBOURS");

        if (neighbours.Count > 0)
        {
            CalculateSeperationForce();
            ApplyAlignment();
            ApplyCohession();
        }

        MoveTowardsPlayer();


    }

    private void MoveTowardsPlayer()
    {
        Vector2 dir = (PlayerManager.Instance.transform.position - transform.position).normalized;
        dir += (Vector2)seperationForce.normalized;
        transform.position += (Vector3)dir * moveSpeed * Time.deltaTime * movementFactor;
    }

    private void ApplyCohession()
    {
        // Make them closer to their mean position
        Vector3 averagePosition = Vector3.zero;
        foreach (var neighbour in neighbours)
        {
            averagePosition += neighbour.transform.position;
        }

        averagePosition /= neighbours.Count;

        Vector3 cohesionDirection = (averagePosition - transform.position).normalized;

        seperationForce += cohesionDirection * cohesionWeight;
    }

    private void ApplyAlignment()
    {
        Vector3 neighboursForward = Vector3.zero;
        foreach (var neighbour in neighbours)
        {
            neighboursForward += neighbour.transform.forward;
        }
        if (neighboursForward.magnitude > 0)
        {
            neighboursForward.Normalize();
        }
        seperationForce += neighboursForward * alignmentWeight;

    }

    private void CalculateSeperationForce()
    {
        foreach (var neighbour in neighbours)
        {
            Vector3 dir = (neighbour.transform.position - transform.position);
            var distance = dir.magnitude;
            var away = -dir.normalized;

            if (distance > 0)
            {
                seperationForce += (away / distance) * seperationWeight;
            }
        }

    }

    private void GetNeighbours()
    {
        //Debug.Log("GETTING NEIGHBOURS");
        neighbours.Clear();
        Physics2D.OverlapCircle(transform.position,
                                       detectionRange,
                                       contactFilter,
                                       neighbours);
    }

    public virtual void TakeDamage(int damageTook)
    {
        Debug.Log($"{gameObject.name} took damage: {damageTook}");
        health -= damageTook;
        DamageVisual(damageTook);
        if (health <= 0)
        {
            Die();
        }
    }

    private void DamageVisual(int damageTook)
    {
        
    }

    public virtual void Die()
    {
        transform.SetParent(null);
        Destroy(gameObject);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
    }

    public void SlowDown(float factor, float duration)
    {
        StartCoroutine(SlowRoutine(factor, duration));
    }

    private IEnumerator SlowRoutine(float factor, float duration)
    {
        movementFactor = factor;
        yield return new WaitForSeconds(duration);
        movementFactor = 1f;
    }
}
