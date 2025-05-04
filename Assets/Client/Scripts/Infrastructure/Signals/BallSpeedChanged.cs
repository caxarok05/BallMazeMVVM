using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
