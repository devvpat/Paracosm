using UnityEngine;

namespace ACoolTeam
{
    public class PlayerCrawlState : PlayerBaseState
    {
        public PlayerCrawlState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory) : base(currentContext, playerStateFactory)
        {
            IsRootState = true;
            InitializeSubState();
        }

        public override void EnterState()
        {
            Debug.Log("Entered Crawl State");
            Ctx.IsGrounded = true;
            Ctx.PlayerSpeed = Ctx.BaseSpeed * Ctx.CrawSpeedMultipler;
        }

        public override void UpdateState()
        {
            CheckSwitchStates();
        }

        public override void ExitState()
        {
            Ctx.PlayerSpeed = Ctx.BaseSpeed;
        }

        public override void CheckSwitchStates()
        {
            if (!Ctx.IsCrawling)
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
