using ActorMono;
using Factory;
using UnityEngine;

namespace Pool
{
    [CreateAssetMenu(fileName = "NewBulletPool", menuName = "Pool/Bullet Pool")]
    public class BulletPoolSO : ComponentPoolSO<BulletMono>
    {
        public override IFactory<BulletMono> Factory { get; set; }
    }
}