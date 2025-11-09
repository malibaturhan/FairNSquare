using UnityEngine;

[CreateAssetMenu(menuName = "Gun Behaviours/Shotgun")]
public class ShotgunBehaviour : GunBehaviour
{
    public int pelletCount = 5;
    public float spreadAngle = 15f;
    public float bulletSpeed = 8f;

    public override void Fire(GunSlot slot, Vector2 direction, Transform bulletContainer)
    {
        if (slot == null || slot.GunInSlot.bulletPrefab == null)
            return;

        for (int i = 0; i < pelletCount; i++)
        {
            float randomSpread = Random.Range(-spreadAngle, spreadAngle);
            Quaternion rotation = Quaternion.Euler(0, 0, randomSpread);
            Vector2 spreadDir = rotation * direction;

            GameObject bullet = Object.Instantiate(slot.GunInSlot.bulletPrefab,
                                                   slot.transform.position,
                                                   Quaternion.identity);
            bullet.transform.parent = bulletContainer;
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            if (rb != null)
                rb.linearVelocity = spreadDir * bulletSpeed;
        }

    }
}
