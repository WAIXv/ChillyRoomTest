using System;
using UnityEngine;

namespace Charactor.Enemy
{
    [RequireComponent(typeof(Rigidbody2D),typeof(BoxCollider2D),typeof(Damageable))]
    public class EnemyMono : MonoBehaviour
    {
        [SerializeField] private float moveSpeed = 7f;
        
        private BoxCollider2D _detectBox;
        private Rigidbody2D _rigid;
        private Damageable _damageable;

        private void Awake()
        {
            _detectBox = transform.Find("DetectBox").GetComponent<BoxCollider2D>();
            _rigid = GetComponent<Rigidbody2D>();
            _damageable = GetComponent<Damageable>();
        }

        private void FixedUpdate()
        {
            _rigid.velocity = transform.right * moveSpeed;
            
            //检测是否碰到墙壁
            if (_detectBox.IsTouchingLayers(1 << LayerMask.NameToLayer("Ground")))
            {
                transform.Rotate(0, 180, 0);
            }
        }
    }
}