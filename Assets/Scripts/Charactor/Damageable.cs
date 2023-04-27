using UnityEngine;
using UnityEngine.Events;

namespace Charactor
{
    public class Damageable : MonoBehaviour
    {
        [Header("Health")]
        [SerializeField] private int _initHealth;
        [SerializeField] private HealthSO _currentHealthSO;

        [Header("Combat")] 
        [SerializeField] private float _hitFlashTime;
        [SerializeField] private Material _hitFlashMaterial;

        private SpriteRenderer _spriteRenderer;
        
        private Material _originMat;

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
            
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _originMat = _spriteRenderer.material;
            
            Ondie += () =>
            {
                Debug.Log(gameObject.name + " Died");
                _currentHealthSO.RestoreHealth(_initHealth);
            };
        }

        public void ReceiveAnAttack(int delta)
        {
            if (IsDead)
                return;
            
            //伤害判定
            _currentHealthSO.ApplyDamage(delta);
            
            //受击闪烁
            _spriteRenderer.material = _hitFlashMaterial;
            Invoke(nameof(HitFlashOff), _hitFlashTime);
            
            if(_currentHealthSO.CurrentHealth <= 0)
            {
                IsDead = true;
                Ondie.Invoke();
            }
            // Debug.Log(gameObject.name + " Received Attack: 【Value】" + delta + ",【NewHealth】" + _currentHealthSO.CurrentHealth);
        }
        
        private void HitFlashOff() => _spriteRenderer.material = _originMat;

    }
}