using System;
using Input;
using UnityEngine;
using UnityEngine.Serialization;

namespace Charactor
{
    public class PlayerGun : MonoBehaviour
    {
        #region 基础组件处理

        [SerializeField] private InputReader _inputReader;
        [SerializeField] private PlayerData _data;
        
        private bool _primaryInput;
        private bool _secondaryAttackInput;
        
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

        [SerializeField] private Transform _smoothTarget;
        [SerializeField] private float smoothTime = 0.01f;
        
        private float velocity;
        private Vector2 _smoothPos;

        private void Update()
        {
            SmoothLocalPos();
            
            
        }

        private void SmoothLocalPos()
        {
            _smoothPos.x = _smoothTarget.position.x;
            _smoothPos.y = Mathf.SmoothDamp(transform.position.y, _smoothTarget.position.y, ref velocity, smoothTime);
            transform.position = _smoothPos;
        }
    }
}