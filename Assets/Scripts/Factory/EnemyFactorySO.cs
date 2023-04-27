using Charactor.Enemy;
using UnityEngine;

namespace Factory
{
    [CreateAssetMenu(fileName = "NewEnemyFactory", menuName = "Factory/Enemy Factory")]
    public class EnemyFactorySO : FactorySO<EnemyBrain>
    {
        [SerializeField] private EnemyBrain _prefab = default;
        
        public override EnemyBrain Create()
        {
            return Instantiate(_prefab);
        }
    }
}