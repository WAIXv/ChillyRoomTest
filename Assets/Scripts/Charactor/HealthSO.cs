using Unity.Collections;
using UnityEngine;

namespace Charactor
{
    [CreateAssetMenu(fileName = "PlayerHealth", menuName = "Game/Player Health", order = 0)]
    public class HealthSO : ScriptableObject
    {
        [SerializeField][ReadOnly] private int _maxHealth;
        [SerializeField][ReadOnly] private int _currentHealth;
        
        public int MaxHealth => _maxHealth;
        public int CurrentHealth => _currentHealth;

        public void SetMaxHealth(int newValue)
        {
            _maxHealth = newValue;
        }

        public void SetCurrentHealth(int newValue)
        {
            _currentHealth = newValue;
        }
	
        public void ApplyDamage(int DamageValue)
        {
            _currentHealth -= DamageValue;
        }

        public void RestoreHealth(int HealthValue)
        {
            _currentHealth += HealthValue;
            if(_currentHealth > _maxHealth)
                _currentHealth = _maxHealth;
        }
    }
}