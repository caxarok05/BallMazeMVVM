
namespace Client.Scripts.Infrastructure.Signals
{
    public class BallRotationChanged
    {
        public float Rotation { get; private set; }

        public BallRotationChanged(float rotation)
        {
            Rotation = rotation;
        }
    }
}
