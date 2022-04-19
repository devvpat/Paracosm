using UnityEngine;

namespace ACoolTeam
{
    public class PlayerJumpState : PlayerBaseState
    {
        public PlayerJumpState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory) : base(currentContext, playerStateFactory)
        {
            IsRootState = true;
            InitializeSubState();
        }

        public override void EnterState()
        {
            Debug.Log("Entered Jump State");
        }

        public override void UpdateState()
        {
            CheckSwitchStates();
        }

        public override void ExitState()
        {

        }

        public override void CheckSwitchStates()
        {

        }

        public override void InitializeSubState()
        {

        }
    }
}
