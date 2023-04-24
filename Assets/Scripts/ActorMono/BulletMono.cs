using System;
using Charactor;
using Pool;
using UnityEngine;
using UnityEngine.Serialization;

namespace ActorMono
{
    [RequireComponent(typeof(SpriteRenderer), typeof(CapsuleCollider2D),typeof(Attacker))]
    public class BulletMono : MonoBehaviour
    {
        [SerializeField] private Sprite _sprite;
        [SerializeField] private float _speed;
        [SerializeField] private BulletPoolSO _pool;

        private Attacker _attacker;
        private SpriteRenderer _spriteRenderer;
        private Rigidbody2D _rigidbody;
        private CapsuleCollider2D _coll;
        
        private void Awake()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _rigidbody = GetComponent<Rigidbody2D>();
            _coll = GetComponent<CapsuleCollider2D>();
            _attacker = GetComponent<Attacker>();
            
            _spriteRenderer.sprite = _sprite;
            _coll.isTrigger = true;
            _coll.direction = CapsuleDirection2D.Horizontal;
        }

        private void OnEnable() => _attacker.AttackEvent += ReturnPool;

        private void OnDisable() => _attacker.AttackEvent -= ReturnPool;

        private void Update()
        {
            transform.position += _speed * Time.deltaTime * transform.right;
        }

        private void ReturnPool()
        {
            _pool.Return(this);
        }
    }
}