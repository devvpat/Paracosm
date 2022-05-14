
using UnityEngine;

namespace ACoolTeam
{
    public class PlayerMoveState : PlayerBaseState
    {
        public PlayerMoveState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory) : base(currentContext, playerStateFactory) { }

        public override void EnterState()
        {
            //Debug.Log("Entered Move State");
            AnimHandle();
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
            if (!Ctx.IsMovementPressed)
                SwitchState(Factory.Idle());
        }

        public override void InitializeSubState()
        {

        }

        private void AnimHandle()
        {
            if (!Ctx.IsJumping)
            {
                if (!Ctx.IsCrawling)
                    AnimationManager.ChangeAnimState(Ctx.PlayerAnimator, Ctx.PlayerWalkAnim);
                else
                    AnimationManager.ChangeAnimState(Ctx.PlayerAnimator, Ctx.PlayerCrawlMoveAnim);
            }
        }
    }
}
