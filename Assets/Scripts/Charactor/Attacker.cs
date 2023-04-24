using UnityEngine;
using UnityEngine.Events;

namespace Charactor
{
    public class Attacker : MonoBehaviour
    {
        public int attackValue;
        
        public event UnityAction AttackEvent = delegate { };
        
        public void OnAttack(bool enable, GameObject other)
        {
            if (!enable) return;
            if (other.CompareTag(tag)) return;

            if (other.TryGetComponent(out Damageable damagable))
            {
                damagable.ReceiveAnAttack(attackValue);
                AttackEvent.Invoke();
            }
        }
    }
}