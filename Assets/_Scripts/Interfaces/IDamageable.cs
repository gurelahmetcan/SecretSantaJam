using UnityEngine;

namespace SantaProject
{
    public interface IDamageable
    {
        public void Damage(int dmg, Transform hit, bool canKnockBack);
    }
}

