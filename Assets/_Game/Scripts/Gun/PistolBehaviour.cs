using UnityEngine;

[CreateAssetMenu(fileName = "PistolBehaviour", menuName = "Gun Behaviours/PistolBehaviour")]
public class PistolBehaviour : GunBehaviour
{
    public float bulletSpeed = 12.0f;
    public override void Fire(GunSlot slot, Vector2 direction, Transform bulletContainer)
    {
        if(slot == null || slot.GunInSlot.bulletPrefab == null)
        {
            Debug.LogWarning("returned from fire");
            return;
        }
        GameObject bullet = Instantiate(slot.GunInSlot.bulletPrefab, 
                                        slot.transform.position,
                                        Quaternion.identity);
        bullet.transform.parent = bulletContainer;
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        if (rb != null) 
        {
            rb.linearVelocity = bulletSpeed * direction;
            Debug.Log("Bullet spawned from pistol");
        }
        else
        {

            Debug.Log("pistol bullet has no rigidbody-!!!!!!!!!!!!!!!!!!!");
        }
    }
}
