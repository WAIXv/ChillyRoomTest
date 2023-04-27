using Pool;
using UnityEngine;

namespace ActorMono
{
    public class SelfReturnMono : MonoBehaviour
    {
        [SerializeField] private float _lifeTime = 10f;
        [SerializeField] private SelfReturnPoolSO _pool;
        
        private void OnEnable()
        {
            Invoke(nameof(SelfReturn), _lifeTime);
        }
        
        private void SelfReturn()
        {
            _pool.Return(this);
            
        }
    }
}