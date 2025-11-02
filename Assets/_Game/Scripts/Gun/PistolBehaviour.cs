using UnityEngine;

[CreateAssetMenu(fileName = "PistolBehaviour", menuName = "Gun Behaviours/PistolBehaviour")]
public class PistolBehaviour : GunBehaviour
{
    public int bulletSpeed = 12;
    public override void Fire(GunSlot slot, Vector2 direction, Transform bulletContainer)
    {
        if(slot != null || slot.GunInSlot.bulletPrefab == null)
        {
            return;
        }
        GameObject bullet = Instantiate(slot.GunInSlot.bulletPrefab, 
                                        slot.transform.position,
                                        Quaternion.identity);
        bullet.transform.parent = bulletContainer;
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        if (rb != null) 
        {
            rb.linearVelocity = bulletSpeed * direction;
        }
    }
}
