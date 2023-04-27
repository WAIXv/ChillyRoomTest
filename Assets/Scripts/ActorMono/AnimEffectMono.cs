using System.Collections;
using Pool;
using UnityEngine;

namespace ActorMono
{
    [RequireComponent(typeof(Animator),typeof(SpriteRenderer))]
    public class AnimEffectMono : MonoBehaviour
    {
        [SerializeField] private bool _destoryAfterAnim = true;
        [SerializeField] private string _animStateName = "Default";
        [SerializeField] private RuntimeAnimatorController _animController;
        [SerializeField] private AnimEffectPoolSO _pool;

        private Animator _animator;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        private void OnEnable()
        {
            _animator.enabled = true;
            PlayeAnim();
        }

        private void PlayeAnim()
        {
            _animator.runtimeAnimatorController = _animController;
            _animator.Play(_animStateName);

            if (_destoryAfterAnim)
                StartCoroutine(ReturnToPoolAfterTime());
        }

        private void ReturnToPool()
        {
            _pool.Return(this);
            // enabled = false;
        }

        private IEnumerator ReturnToPoolAfterTime()
        {
            var waitTime = new WaitForSeconds(0.05f);
            while (_animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 0.95f)
            {
                yield return waitTime;
            }
            ReturnToPool();
        }
    }
}