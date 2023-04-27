using ActorMono;
using Factory;
using UnityEngine;

namespace Pool
{
    [CreateAssetMenu(fileName = "NewSelfReturnPool", menuName = "Pool/Self Return Pool")]
    public class SelfReturnPoolSO : ComponentPoolSO<SelfReturnMono>
    {
        [SerializeField] private SelfReturnFactorySO _factory;
        public int _initialPoolSize = 40;

        public override IFactory<SelfReturnMono> Factory
        {
            get => _factory;
            set => _factory = (SelfReturnFactorySO)value;
        }
    }
}