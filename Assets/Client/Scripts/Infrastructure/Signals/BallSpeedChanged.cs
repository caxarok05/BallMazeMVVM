namespace Client.Scripts.Infrastructure.Signals
{
    public class BallSpeedChanged
    {
        public float Speed { get; private set; }

        public BallSpeedChanged(float speed)
        {
            Speed = speed;
        }
    }
}
