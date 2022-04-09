using UnityEngine;

namespace ACoolTeam
{
    public class PlayerStateMachine : MonoBehaviour
    {
        #region Fields
        // hsm refs
        private PlayerBaseState _currentState;
        private PlayerStateFactory _states;

        // stuff we probably need (rename)
        private bool _isMovementPressed;
        private bool _isGrounded;
        private bool _isJumping;

        #endregion

        #region Properties
        public PlayerBaseState CurrentState { get { return _currentState; } set { _currentState = value; } }
        public bool IsMovementPressed { get { return _isMovementPressed; } }
        public bool IsGrounded { get { return _isGrounded; } set { _isGrounded = value; } }
        public bool IsJumping { get { return _isJumping; } set { _isJumping = value; } }

        #endregion

        private void Awake()
        {
            _states = new PlayerStateFactory(this);

            // always start player in the grounded state
            _currentState = _states.Grounded();
            _currentState.EnterState();

            // player Input initialization

        }

        private void Update()
        {
            _currentState.UpdateStates();
        }
    }
}
