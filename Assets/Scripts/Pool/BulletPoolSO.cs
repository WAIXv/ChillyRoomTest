using ActorMono;
using Factory;
using UnityEngine;

namespace Pool
{
    [CreateAssetMenu(fileName = "NewBulletPool", menuName = "Pool/Bullet Pool")]
    public class BulletPoolSO : ComponentPoolSO<BulletMono>
    {
        [SerializeField] private BulletFactorySO _factory;
        public int _initialPoolSize = 40;

        public override IFactory<BulletMono> Factory
        {
            get => _factory;
            set => _factory = (BulletFactorySO)value;
        }
    }
}