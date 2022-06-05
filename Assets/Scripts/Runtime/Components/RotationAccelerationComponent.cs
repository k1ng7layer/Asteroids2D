using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Runtime.Components
{
    public struct RotationAccelerationComponent
    {
        public float speed;
        public float acelerationMultiplier;
        public bool accelerate;
        public float maxSpeed;
        public int direction;
    }
}
