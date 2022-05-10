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
            Ctx.IsJumping = true;
            Ctx.IsGrounded = false;
            Ctx.CanJump = false;
            Ctx.JumpCooldown = 1f;
            Ctx.PlayerRigidBody.velocity = Vector2.up * Ctx.JumpVelocity;
        }

        public override void UpdateState()
        {

            CheckSwitchStates();
        }

        public override void ExitState()
        {
            Ctx.IsJumping = false;
            Ctx.IsGrounded = true;
        }

        public override void CheckSwitchStates()
        {
            if (Ctx.PlayerRigidBody.velocity.y >= Mathf.Min(0.1f, -0.001f) && Ctx.PlayerRigidBody.velocity.y <= Mathf.Max(0.001f, -0.1f))
                SwitchState(Factory.Grounded());
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
