
using UnityEngine;

namespace ACoolTeam
{
    public class PlayerIdleState : PlayerBaseState
    {
        public PlayerIdleState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory) : base(currentContext, playerStateFactory) { }

        public override void EnterState()
        {
            Debug.Log("Entered Idle State");

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
            if (Ctx.IsMovementPressed)
                SwitchState(Factory.Move());
        }

        public override void InitializeSubState()
        {

        }

        private void AnimHandle()
        {
            if(!Ctx.IsCrawling)
                AnimationManager.ChangeAnimState(Ctx.PlayerAnimator, Ctx.PlayerIdleAnim);
            else
                AnimationManager.ChangeAnimState(Ctx.PlayerAnimator, Ctx.PlayerCrawlIdleAnim);
        }
    }
}
