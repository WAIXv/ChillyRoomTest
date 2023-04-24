using System;
using Charactor;
using Pool;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

namespace ActorMono
{
    [RequireComponent(typeof(SpriteRenderer), typeof(CapsuleCollider2D),typeof(Attacker))]
    public class BulletMono : MonoBehaviour
    {
        [SerializeField] private LayerMask _layers;
        [SerializeField] private Sprite _sprite;
        [SerializeField] private float _speed;
        [SerializeField] private BulletMonoPoolSO _pool;

        private Attacker _attacker;
        private SpriteRenderer _spriteRenderer;
        private CapsuleCollider2D _coll;

        private Vector3 _previousPos = new(0,0,-100f);
        private float _step;
        private RaycastHit2D _hitInfo;
        
        private void Awake()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _coll = GetComponent<CapsuleCollider2D>();
            _attacker = GetComponent<Attacker>();

            _spriteRenderer.sprite = _sprite;
            _coll.isTrigger = true;
            _coll.direction = CapsuleDirection2D.Horizontal;
            _step = _speed * Time.fixedDeltaTime;
        }

        private void FixedUpdate()
        {
            _previousPos = transform.position;
            transform.position += _speed * Time.fixedDeltaTime * transform.right;
            
            _hitInfo = Physics2D.Raycast(_previousPos,transform.right,
                _step,_layers,0,0);
            if (!_hitInfo) return;
            _attacker.OnAttack(true, _hitInfo.collider.gameObject);
            _pool.Return(this);

        }
    }
}