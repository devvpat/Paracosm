
namespace ACoolTeam
{
    public class PlayerStateFactory
    {
        private PlayerStateMachine _context;

        public PlayerStateFactory(PlayerStateMachine currentContext)
        {
            _context = currentContext;
        }
        public PlayerBaseState Grounded()
        {
            return new PlayerGroundedState(_context, this);
        }
        public PlayerBaseState Crawling()
        {
            return new PlayerCrawlState(_context, this);
        }
        public PlayerBaseState Jump()
        {
            return new PlayerJumpState(_context, this);
        }

        public PlayerBaseState Idle()
        {
            return new PlayerIdleState(_context, this);
        }
        public PlayerBaseState Move()
        {
            return new PlayerMoveState(_context, this);
        }
    }
}
