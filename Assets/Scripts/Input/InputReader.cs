using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace Input
{
    [CreateAssetMenu(fileName = "InputReader", menuName = "Game/Input Reader")]
    public class InputReader : ScriptableObject,GameInput.IGamePlayActions
    {
        //GamePlay
        public event UnityAction JumpEvent = delegate { };
        public event UnityAction JumpCanceledEvent = delegate { };
        public event UnityAction PrimaryAttackEvent = delegate { };
        public event UnityAction PrimaryAttackCanceledEvent = delegate { };
        public event UnityAction SecondaryAttackEvent = delegate { };
        public event UnityAction SecondaryAttackCanceledEvent = delegate { };
        public event UnityAction<float> MoveEvent = delegate { };

        private GameInput _gameInput;

        private void OnEnable()
        {
            if(_gameInput == null)
            {
                _gameInput = new GameInput();
                _gameInput.GamePlay.SetCallbacks(this);
            }
            _gameInput.GamePlay.Enable();
        }
        
        private void OnDisable()
        {
            DisableAllInput();
        }


        public void OnMove(InputAction.CallbackContext context)
        {
            MoveEvent?.Invoke(context.ReadValue<float>());
        }

        public void OnJump(InputAction.CallbackContext context)
        {
            switch (context.phase)
            {
                case InputActionPhase.Performed:
                    JumpEvent.Invoke();
                    break;
                case InputActionPhase.Canceled:
                    JumpCanceledEvent.Invoke();
                    break;
            }
        }

        public void OnPrimaryAttack(InputAction.CallbackContext context)
        {
            switch (context.phase)
            {
                case InputActionPhase.Performed:
                    PrimaryAttackEvent.Invoke();
                    break;
                case InputActionPhase.Canceled:
                    PrimaryAttackCanceledEvent.Invoke();
                    break;
            }
        }

        public void OnSecondaryAttack(InputAction.CallbackContext context)
        {
            switch (context.phase)
            {
                case InputActionPhase.Performed:
                    SecondaryAttackEvent.Invoke();
                    break;
                case InputActionPhase.Canceled:
                    SecondaryAttackCanceledEvent.Invoke();
                    break;
            }
        }
        
        public void DisableAllInput()
        {
            _gameInput.GamePlay.Disable();
        }
    }
}