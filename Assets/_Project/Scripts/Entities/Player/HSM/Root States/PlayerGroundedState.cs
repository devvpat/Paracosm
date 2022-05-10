using UnityEngine;

namespace ACoolTeam
{
    public class PlayerGroundedState : PlayerBaseState
    {
        public PlayerGroundedState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory) : base(currentContext, playerStateFactory)
        {
            IsRootState = true;
            InitializeSubState();
        }

        public override void EnterState()
        {
            Debug.Log("Entered Grounded State");
            Ctx.IsGrounded = true;
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
            if (Ctx.IsGrounded && !Ctx.IsJumping && Ctx.IsJumpPressed && Ctx.CanJump)
                SwitchState(Factory.Jump());
            else if(Ctx.IsCrawling)
                SwitchState(Factory.Crawling());

        }

        public override void InitializeSubState()
        {
            if (!Ctx.IsMovementPressed)
                SetSubState(Factory.Idle());
            else
                SetSubState(Factory.Move());
        }
    }
}
