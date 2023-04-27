using ActorMono;
using UnityEngine;

namespace Factory
{
    [CreateAssetMenu(fileName = "NewAnimEffectFactory",menuName = "Factory/Anim Effect Factory")]
    public class AnimEffectFactorySO : FactorySO<AnimEffectMono>
    {
        public AnimEffectMono prefab = default;
        public override AnimEffectMono Create()
        {
            return Instantiate(prefab);
        }
    }
}