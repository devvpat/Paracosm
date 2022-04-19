
using UnityEngine;

namespace ACoolTeam
{
    public class PlayerIdleState : PlayerBaseState
    {
        public PlayerIdleState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory) : base(currentContext, playerStateFactory) { }

        public override void EnterState()
        {
            Debug.Log("Entered Idle State");
            AnimationManager.ChangeAnimState(Ctx.PlayerAnimator, Ctx.PlayerIdleAnim);
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
