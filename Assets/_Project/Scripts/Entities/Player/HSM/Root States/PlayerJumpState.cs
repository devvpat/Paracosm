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
            float jumpVelocity = 15;
            Ctx.PlayerRigidBody.velocity = Vector2.up * jumpVelocity;
        }

        public override void UpdateState()
        {

            HandleGravity();
            CheckSwitchStates();
        }

        public override void ExitState()
        {
            Ctx.IsJumping = false;
        }

        public override void CheckSwitchStates()
        {
            if (Ctx.PlayerRigidBody.velocity.sqrMagnitude == 0)
                SwitchState(Factory.Grounded());
        }

        public override void InitializeSubState()
        {
            if (!Ctx.IsMovementPressed)
                SetSubState(Factory.Idle());
            else
                SetSubState(Factory.Move());
        }

        private void HandleGravity()
        {
            //Ctx.PlayerPosition += new Vector2(Ctx.PlayerPosition.x, Ctx.PlayerPosition.y - Ctx.Gravity);
        }
    }
}
