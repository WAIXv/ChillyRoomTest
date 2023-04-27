using Charactor.Enemy;
using Factory;
using UnityEngine;

namespace Pool
{
    [CreateAssetMenu(fileName = "NewEnemyPool", menuName = "Pool/Enemy Pool")]
    public class EnemyPoolSO : ComponentPoolSO<EnemyBrain>
    {
        [SerializeField] private EnemyFactorySO _factory;
        public int _initialPoolSize = 40;

        public override IFactory<EnemyBrain> Factory
        {
            get => _factory;
            set => _factory = (EnemyFactorySO)value;
        }
    }
}