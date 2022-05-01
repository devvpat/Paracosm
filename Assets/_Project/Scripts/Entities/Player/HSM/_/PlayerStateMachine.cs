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
        [SerializeField] private Rigidbody2D _playerRigidBody;
        [SerializeField] private SpriteRenderer _playerSpriteRenderer;
        [SerializeField] private Animator _playerAnimator;

        // player animation
        [SerializeField] private AnimationClip _playerWalkAnim;
        [SerializeField] private AnimationClip _playerIdleAnim;

        // state params
        private bool _isMovementPressed;
        private bool _isActionPressed;
        private bool _isGrounded;

        // jump vars
        private bool _isJumpPressed = false;
        private bool _isJumping;
        [SerializeField] 
        private float _jumpVelocity;

        // crawl vars
        private bool _isCrawling = false;
        [SerializeField]
        private float _crawlSpeedMultipler;

        // player params
        [SerializeField] 
        private float _speed;
        private float _baseSpeed;

        // vecs
        private Vector2 _currentMovementInput;

        #endregion

        #region Properties
        public PlayerBaseState CurrentState { get { return _currentState; } set { _currentState = value; } }
        public Rigidbody2D PlayerRigidBody { get { return _playerRigidBody; } set { _playerRigidBody = value; } }
        public SpriteRenderer PlayerSpriteRenderer { get { return _playerSpriteRenderer; } }
        public Animator PlayerAnimator { get { return _playerAnimator; } }
        public AnimationClip PlayerIdleAnim { get { return _playerIdleAnim; } }
        public AnimationClip PlayerWalkAnim { get { return _playerWalkAnim; } }
        public bool IsMovementPressed { get { return _isMovementPressed; } }
        public bool IsActionPressed { get { return _isActionPressed; } }
        public bool IsJumpPressed { get { return _isJumpPressed; } }
        public bool IsGrounded { get { return _isGrounded; } set { _isGrounded = value; } }
        public bool IsJumping { get { return _isJumping; } set { _isJumping = value; } }
        public bool IsCrawling { get { return _isCrawling; } }
        public float JumpVelocity { get { return _jumpVelocity; } set { _jumpVelocity = value; } }
        public float PlayerSpeed { get { return _speed; } set { _speed = value; } }
        public float BaseSpeed { get { return _baseSpeed; } }
        public float CrawSpeedMultipler { get { return _crawlSpeedMultipler; } }
        public Vector2 CurrentMovement { get { return _currentMovementInput; } set { _currentMovementInput = value; } }

        #endregion

        #region Unity Methods
        private void Awake()
        {
            // inits
            _states = new PlayerStateFactory(this);
            _playerInput = new PlayerInput();
            _baseSpeed = _speed;

            // always start player in the grounded state
            _currentState = _states.Grounded();
            _currentState.EnterState();

            // player Input initialization
            _playerInput.Player.Move.started += OnMoveInput;
            _playerInput.Player.Move.performed += OnMoveInput;
            _playerInput.Player.Move.canceled += OnMoveInput;
            _playerInput.Player.Jump.started += OnJumpInput;
            _playerInput.Player.Jump.canceled += OnJumpInput;
            _playerInput.Player.Crawl.started += OnCrawlInput;
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
            _playerRigidBody.velocity = new Vector2(_currentMovementInput.x * _speed, _playerRigidBody.velocity.y);
            _currentState.UpdateStates();
        }

        #endregion

        #region Input Methods
        private void OnMoveInput(InputAction.CallbackContext context)
        {
            _currentMovementInput = context.ReadValue<Vector2>();
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

        private void OnCrawlInput(InputAction.CallbackContext context)
        {
            _isCrawling = !_isCrawling;
        }

        #endregion
    }
}
