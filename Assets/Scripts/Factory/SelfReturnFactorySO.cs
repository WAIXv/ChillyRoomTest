using ActorMono;
using UnityEngine;

namespace Factory
{
    [CreateAssetMenu(fileName = "NewSelfReturnFactory", menuName = "Factory/Self Return Factory")]
    public class SelfReturnFactorySO : FactorySO<SelfReturnMono>
    {
        [SerializeField] private SelfReturnMono _prefab;
        
        public override SelfReturnMono Create()
        {
            return Instantiate(_prefab);
        }
    }
}