using UnityEngine;

public class Bullet : MonoBehaviour, IMovable
{
    [Header("***Settings***")]
    [SerializeField] private float speed;
    [SerializeField] private int damage;
    [SerializeField] private float lifetime;
    [SerializeField] private Vector2 direction;

    [Header("***Combat Stats***")]
    [SerializeField, Range(0f, 1f)] private float criticalChance;
    [SerializeField] private float criticalDamageMultiplier = 2f;
    [SerializeField] private int passThroughCount;

    void Start()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<IDamagable>(out IDamagable target))
        {
            Debug.Log($"Bullet deals damage to {target}");
            target.TakeDamage(damage);
            gameObject.transform.SetParent(null);
            Destroy(gameObject);
        }
    }

    public void SetData(
        float speed,
        int damage,
        float lifetime,
        Vector2 direction,
        float criticalChance,
        float criticalDamageMultiplier,
        int passThroughCount)
    {
        this.speed = speed;
        this.damage = damage;
        this.lifetime = lifetime;
        this.direction = direction;
        this.criticalChance = criticalChance;
        this.criticalDamageMultiplier = criticalDamageMultiplier;
        this.passThroughCount = passThroughCount;
    }

    public void Move(Vector2 direction, float speed)
    {
        throw new System.NotImplementedException();
    }
}
