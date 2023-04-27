using ActorMono;
using Factory;
using UnityEngine;

namespace Pool
{
    [CreateAssetMenu(fileName = "NewAnimEffectPool", menuName = "Pool/Anim Effect Pool")]
    public class AnimEffectPoolSO : ComponentPoolSO<AnimEffectMono>
    {
        [SerializeField] private AnimEffectFactorySO _factory;
        public int _initialPoolSize = 40;

        public override IFactory<AnimEffectMono> Factory
        {
            get => _factory;
            set => _factory = (AnimEffectFactorySO)value;
        }
    }
}