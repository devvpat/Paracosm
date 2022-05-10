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
            Ctx.BodyCollider.enabled = false;
            Ctx.CrawlCollider.enabled = true;
            AnimHandle();
        }

        public override void UpdateState()
        {
            CheckSwitchStates();
        }

        public override void ExitState()
        {
            Ctx.PlayerSpeed = Ctx.BaseSpeed;
            Ctx.BodyCollider.enabled = true;
            Ctx.CrawlCollider.enabled = false;
            AnimHandle();
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

        private void AnimHandle()
        {
            if (!Ctx.IsCrawling)
            {
                if(Ctx.IsMovementPressed)
                    AnimationManager.ChangeAnimState(Ctx.PlayerAnimator, Ctx.PlayerWalkAnim);
                else
                    AnimationManager.ChangeAnimState(Ctx.PlayerAnimator, Ctx.PlayerIdleAnim);
            }
            else
            {
                if (Ctx.IsMovementPressed)
                    AnimationManager.ChangeAnimState(Ctx.PlayerAnimator, Ctx.PlayerCrawlMoveAnim);
                else
                    AnimationManager.ChangeAnimState(Ctx.PlayerAnimator, Ctx.PlayerCrawlIdleAnim);
            }
        }
    }
}
