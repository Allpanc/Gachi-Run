public class PlayerStateFactory
{
    Player _context;

    public PlayerStateFactory(Player context)
    {
        _context = context;
    }

    public PlayerBaseState Run() => new RunningState();
    public PlayerBaseState Jump() => new RunningState();
    public PlayerBaseState DoubleJump() => new RunningState();
}
