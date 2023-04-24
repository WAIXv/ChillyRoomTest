using System;
using Event;
using Input;
using Pool;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

namespace Charactor
{
    public class PlayerGun : MonoBehaviour
    {
        #region 基础组件&输入处理

        [SerializeField] private InputReader _inputReader;
        [SerializeField] private PlayerData _data;
        
        private bool _primaryInput;
        private bool _secondaryAttackInput;
        private UnityEngine.Camera _camera;
        
        public bool PrimaryInput
        {
            get => _primaryInput;
            set => _primaryInput = value;
        }
        public bool SecondaryAttackInput
        {
            get => _secondaryAttackInput;
            set => _secondaryAttackInput = value;
        }
        public Vector2 MousePositon => _camera.ScreenToWorldPoint(Mouse.current.position.ReadValue());

        private void Awake()
        {
            _camera = UnityEngine.Camera.main;
            _bulletPool.Prewarm(_bulletPool._initialPoolSize);
        }

        private void OnEnable()
        {
            _inputReader.PrimaryAttackEvent += OnPrimaryAttack;
            _inputReader.PrimaryAttackCanceledEvent += OnPrimaryAttackCancled;
            _inputReader.SecondaryAttackEvent += OnSecondaryAttack;
            _inputReader.SecondaryAttackCanceledEvent += OnSecondaryAttackCancled;
        }
        
        private void OnDisable()
        {
            _inputReader.PrimaryAttackEvent -= OnPrimaryAttack;
            _inputReader.PrimaryAttackCanceledEvent -= OnPrimaryAttackCancled;
            _inputReader.SecondaryAttackEvent -= OnSecondaryAttack;
            _inputReader.SecondaryAttackCanceledEvent -= OnSecondaryAttackCancled;
        }

        private void OnPrimaryAttack() => _primaryInput = true;
        private void OnPrimaryAttackCancled() => _primaryInput = false;
        private void OnSecondaryAttack() => _secondaryAttackInput = true;
        private void OnSecondaryAttackCancled() => _secondaryAttackInput = false;
        
        #endregion

        [SerializeField] private Transform _playerTrans;
        
        [Header("Pos Smooth")]
        [SerializeField] private Transform _smoothTarget;
        [SerializeField] private float smoothTime = 0.01f;

        [Header("PrimaryAttack")] 
        [SerializeField] private Transform _firePoint;
        [SerializeField] private BulletMonoPoolSO _bulletPool;

        [Header("Broadcasting On")] 
        [SerializeField] private Vector3EventChannelSO _cameraShakeEvent;

        private float velocity;
        private Vector2 _smoothPos;
        private float _prevoiusPrimaryAttack = 0f;

        private void Update()
        {
            SmoothLocalPos(); 

            if (PrimaryInput)
            {
                transform.rotation = Quaternion.Euler(0,0,Mathf.Atan2(MousePositon.y - transform.position.y, MousePositon.x - transform.position.x) * Mathf.Rad2Deg);
                PrimaryAttack();
            }
            else
            {
                transform.rotation = Quaternion.Euler(0, transform.right.x > 0 ? 0 : -180, 0);
            }
        }

        private void PrimaryAttack()
        {
            if(Time.time - _prevoiusPrimaryAttack < _data.PrimaryInterval)
                return;
            
            //连发计时器更新
            _prevoiusPrimaryAttack = Time.time;
            
            //相机抖动
            var dir = (MousePositon-(Vector2)transform.position).normalized * _data.PrimaryShakeMultiplier;
            _cameraShakeEvent.RaiseEvent(new Vector3(dir.x, dir.y, _data.PrimaryInterval));
            
            //角色后坐
            _playerTrans.position -= (Vector3)dir;
            
            //子弹发射
            foreach (var fire in _data.PrimaryFireGroup)
            {
                var bullet = _bulletPool.Request().transform;
                bullet.position = _firePoint.position + fire.Offset;
                bullet.rotation = 
                    _firePoint.rotation * Quaternion.Euler(0,0,fire.Angle) * 
                    Quaternion.Euler(0,0,Random.Range(-fire.RandomAngle,fire.RandomAngle));
            }
        }

        private void SmoothLocalPos()
        {
            _smoothPos.x = _smoothTarget.position.x;
            _smoothPos.y = Mathf.SmoothDamp(transform.position.y, _smoothTarget.position.y, ref velocity, smoothTime);
            transform.position = _smoothPos;
        }
    }
}