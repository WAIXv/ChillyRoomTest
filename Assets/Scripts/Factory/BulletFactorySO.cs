using ActorMono;
using UnityEngine;

namespace Factory
{
    [CreateAssetMenu(fileName = "NewBulletFactory", menuName = "Factory/Bullet Factory")]
    public class BulletFactorySO : FactorySO<BulletMono>
    {
        public BulletMono _prefab = default;
        
        public override BulletMono Create()
        {
            return Instantiate(_prefab);
        }
    }
}