using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

namespace Charactor
{
    public class Damageable : MonoBehaviour
    {
        [Header("Health")]
        [SerializeField] private int _initHealth;
        [SerializeField] private HealthSO _currentHealthSO;
        
        // [Header("Combat")]

        public event UnityAction Ondie = delegate { };
        public bool IsDead { get; set; } = false;

        private void Awake()
        {
            if (_currentHealthSO == null)
            {
                _currentHealthSO = ScriptableObject.CreateInstance<HealthSO>();
                _currentHealthSO.SetMaxHealth(_initHealth);
                _currentHealthSO.SetCurrentHealth(_initHealth);
            }
        }

        public void ReceiveAnAttack(int delta)
        {
            if (IsDead)
                return;
            
            _currentHealthSO.ApplyDamage(delta);
            
            if(_currentHealthSO.CurrentHealth <= 0)
            {
                IsDead = true;
                Ondie.Invoke();
            }
            Debug.Log(gameObject.name + " Received Attack: 【Value】" + delta + ",【NewHealth】" + _currentHealthSO.CurrentHealth);
        }
    }
}