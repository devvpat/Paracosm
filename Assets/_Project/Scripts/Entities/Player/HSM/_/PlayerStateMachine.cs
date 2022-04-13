using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace ACoolTeam
{
    public class PlayerStateMachine : MonoBehaviour
    {
        #region Fields
        // hsm refs
        private PlayerBaseState _currentState;
        private PlayerStateFactory _states;

        // input ref
        private PlayerInput _playerInput;

        // player components
        [SerializeField]
        private Rigidbody2D _playerRigidBody;
        [SerializeField]
        private SpriteRenderer _playerSpriteRenderer;

        // state params
        private bool _isMovementPressed;
        private bool _isActionPressed;
        private bool _isJumpPressed;
        private bool _isGrounded;
        private bool _isJumping;

        // player params
        [SerializeField]
        private float _speed;

        // vecs
        private Vector2 _currentMovementInput;

        #endregion

        #region Properties
        public PlayerBaseState CurrentState { get { return _currentState; } set { _currentState = value; } }
        public Rigidbody2D PlayerRigidBody { get { return _playerRigidBody; } }
        public SpriteRenderer PlayerSpriteRenderer { get { return _playerSpriteRenderer; } }
        public bool IsMovementPressed { get { return _isMovementPressed; } }
        public bool IsActionPressed { get { return _isActionPressed; } }
        public bool IsJumpPressed { get { return _isJumpPressed; } }
        public bool IsGrounded { get { return _isGrounded; } set { _isGrounded = value; } }
        public bool IsJumping { get { return _isJumping; } set { _isJumping = value; } }

        #endregion

        #region Unity Methods
        private void Awake()
        {
            // inits
            _states = new PlayerStateFactory(this);
            _playerInput = new PlayerInput();

            // always start player in the grounded state
            _currentState = _states.Grounded();
            _currentState.EnterState();

            // player Input initialization
            _playerInput.Player.Move.started += OnMoveInput;
            _playerInput.Player.Move.performed += OnMoveInput;
            _playerInput.Player.Move.canceled += OnMoveInput;
            _playerInput.Player.Jump.started += OnJumpInput;
            _playerInput.Player.Jump.performed += OnJumpInput;
            _playerInput.Player.Jump.canceled += OnJumpInput;
        }

        private void OnEnable()
        {
            _playerInput.Enable();
        }

        private void OnDisable()
        {
            _playerInput.Disable();
        }

        private void Update()
        {
            _playerRigidBody.MovePosition(_playerRigidBody.position + _currentMovementInput * _speed * Time.fixedDeltaTime);
            _currentState.UpdateStates();
        }

        #endregion

        #region Input Methods
        private void OnMoveInput(InputAction.CallbackContext context)
        {
            _currentMovementInput = context.ReadValue<Vector2>();
            _currentMovementInput.y = 0;
            _isMovementPressed = _currentMovementInput.x != 0 || _currentMovementInput.y != 0;

            if (_currentMovementInput.x > 0)
                _playerSpriteRenderer.flipX = false;
            else if (_currentMovementInput.x < 0)
                _playerSpriteRenderer.flipX = true;
        }

        private void OnJumpInput(InputAction.CallbackContext context)
        {
            _isJumpPressed = context.ReadValue<float>() > 0;
        }

        #endregion
    }
}
