using UnityEngine;

public abstract class GunBehaviour : ScriptableObject
{
    public abstract void Fire(GunSlot slot, Vector2 direction);
}
