using System;
using System.Collections;
using Pool;
using UnityEngine;

namespace Charactor.Enemy
{
    [RequireComponent(typeof(Rigidbody2D),typeof(BoxCollider2D),typeof(Damageable))]
    public class EnemyBrain : MonoBehaviour
    {
        [SerializeField] private float _moveSpeed = 3f;
        [SerializeField] private float _dieFreezeDuration;
        [SerializeField] private float _hitBackForce;
        [SerializeField] private float _dieWaitTime = 1f;
        [SerializeField] private EnemyPoolSO _pool;
        [SerializeField] private SelfReturnPoolSO _dieBodyPool;
        
        private BoxCollider2D _detectBox;
        private BoxCollider2D _hitBox;
        private Rigidbody2D _rigid;
        private Damageable _damageable;
        private Animator _animator;
        private SpriteRenderer _spriteRenderer;
        private static readonly int Die = Animator.StringToHash("Die");

        private void Awake()
        {
            _detectBox = transform.Find("DetectBox").GetComponent<BoxCollider2D>();
            _rigid = GetComponent<Rigidbody2D>();
            _damageable = GetComponent<Damageable>();
            _animator = GetComponent<Animator>();
            _hitBox = GetComponent<BoxCollider2D>();
            _spriteRenderer = GetComponent<SpriteRenderer>();
            
            _dieBodyPool.Prewarm(_dieBodyPool._initialPoolSize);

            _damageable.Ondie += () =>
            {
                _animator.SetTrigger(Die);
                Time.timeScale = 0f;
                StartCoroutine(TimeScaleBack(_dieFreezeDuration));

                StartCoroutine(ReturnToPool());
                
                _hitBox.enabled = false;
                _detectBox.enabled = false;
            };
        }

        private void OnEnable()
        {
            _damageable.IsDead = false;
            _hitBox.enabled = true;
            _detectBox.enabled = true;
        }

        private void FixedUpdate()
        {
            if(!_damageable.IsDead)
            {
                var rigidVelocity = _rigid.velocity;
                rigidVelocity.x = transform.right.x * _moveSpeed;
                _rigid.velocity = rigidVelocity;
                

                //检测是否碰到墙壁
                if (_detectBox.IsTouchingLayers(1 << LayerMask.NameToLayer("Ground")))
                {
                    transform.Rotate(0, 180, 0);
                }
            }
        }
        
        //死亡击退动画帧事件
        private void DieHitBack()
        {
            _rigid.velocity =
                (transform.right.x > 0 
                    ? Quaternion.Euler(0, -180f, 60f) 
                    : Quaternion.Euler(0, -180f, -60f)) 
                * transform.right * _hitBackForce;
        }
        
        private IEnumerator TimeScaleBack(float stopTime)
        {
            var enterTime = Time.unscaledTime;
            while (Time.unscaledTime - enterTime < stopTime)
            {
                yield return null;
            }
            Time.timeScale = 1;
        }

        private IEnumerator ReturnToPool()
        {
            var enterTime = Time.time;
            while (Time.time - enterTime < _dieWaitTime)
            {
                yield return null;
            }

            var go = _dieBodyPool.Request();
            go.transform.position = transform.position;
            go.transform.rotation = transform.rotation;
            var spriteRenderer = go.GetComponent<SpriteRenderer>();
            spriteRenderer.sprite = _spriteRenderer.sprite;
            spriteRenderer.flipX = _spriteRenderer.flipX;
            go.transform.localScale = transform.localScale;
            _pool.Return(this);
        }
    }
}