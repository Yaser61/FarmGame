using UnityEngine;

public interface IA_Hitable
{
    public abstract GameObject Hit(GameObject from, int damage);
}
