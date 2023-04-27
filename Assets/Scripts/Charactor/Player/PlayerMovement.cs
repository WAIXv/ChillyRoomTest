using System;
using Input;
using UnityEngine;

namespace Charactor
{
    public class PlayerMovement : MonoBehaviour
    {
        #region 基础组件&输入处理

        [SerializeField] private InputReader _inputReader;
        [SerializeField] private PlayerData _data;

        private bool _jumpInput;
        private float _moveInput;
        private LayerMask _groundLayer;
        private Collider2D _footBox;

        public Animator animator { get; private set; }
        public Rigidbody2D rigidBody { get; private set; }
        public SpriteRenderer spriteRenderer { get; private set; }
        
        public bool JumpInput
        {
            get => _jumpInput && OnGround;
            set => _jumpInput = value;
        }
        public float MoveInput
        {
            get => _moveInput;
            set => _moveInput = value;
        }
        public bool OnGround => _footBox.IsTouchingLayers(_groundLayer);
        public bool OnFire => _playerGun.PrimaryInput || _playerGun.SecondaryAttackInput;

        private void Awake()
        {
            animator = GetComponent<Animator>();
            rigidBody = GetComponent<Rigidbody2D>();
            spriteRenderer = GetComponent<SpriteRenderer>();
            _groundLayer = 1 << LayerMask.NameToLayer("Ground");
            _footBox = transform.Find("FootBox").GetComponent<Collider2D>();
        }

        private void OnEnable()
        {
            _inputReader.JumpEvent += OnJump;
            _inputReader.JumpCanceledEvent += OnJumpCancled;
            _inputReader.MoveEvent += OnMove;
        }

        private void OnDisable()
        {
            _inputReader.JumpEvent -= OnJump;
            _inputReader.JumpCanceledEvent -= OnJumpCancled;
            _inputReader.MoveEvent -= OnMove;
        }

        #region InputReader回调监听
        
        private void OnJump() => _jumpInput = true;
        private void OnJumpCancled() => _jumpInput = false;
        private void OnMove(float value) => _moveInput = value;
        
        #endregion

        #endregion
        
        [SerializeField] private PlayerGun _playerGun;
        [SerializeField] private Transform _gunTrans;

        private Vector2 _moveVector;
        private float _previousInput = 0f;
        
        private void FixedUpdate()
        {
            Movment();
            Gravity();
            ApplyMoveVector();
        }

        private void Movment()
        {
            animator.SetBool(Ground,OnGround);
            
            _moveVector.x = MoveInput * _data.MoveSpeed;
            animator.SetBool(Move,OnGround && Mathf.Abs(MoveInput) > .02f);

            //跳跃
            if (JumpInput)
            {
                animator.SetBool(Move,false);
                animator.SetTrigger(Jump);
                _moveVector.y = _data.JumpSpeed;
                JumpInput = false;
            }
            
            //开火
            if (OnFire)
            {
                transform.rotation = Quaternion.Euler(0, 
                        _playerGun.MousePositon.x > transform.position.x ? 0 : -180f, 0);
                return;
            }
            
            //不开火转身
            if (Mathf.Abs(MoveInput) > .02f && Math.Abs(MoveInput - _previousInput) > .02f)
            {
                FaceToInput();
                _gunTrans.rotation = Quaternion.Euler(0,MoveInput > 0 ? 0 : -180f ,0);
                _previousInput = MoveInput;
            }
        }

        private void Gravity()
        {
            if(OnGround) return;
            
            _moveVector.y += Physics2D.gravity.y * Time.fixedDeltaTime * _data.GravityMultiplier;
            if (_moveVector.y < _data.ClampSpeed) 
                _moveVector.y = _data.ClampSpeed;
        }

        private void ApplyMoveVector()
        {
            rigidBody.velocity = _moveVector;
        }
        
        private void FaceToInput()
        {
            transform.rotation = Quaternion.Euler(0,MoveInput > 0 ? 0 : -180f ,0);
        }

        private static readonly int Move = Animator.StringToHash("OnMove");
        private static readonly int Jump = Animator.StringToHash("Jump");
        private static readonly int Ground = Animator.StringToHash("OnGround");
    }
}