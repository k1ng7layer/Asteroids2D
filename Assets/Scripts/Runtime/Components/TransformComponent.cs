using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Runtime.Components
{
    public struct TransformComponent
    {
        public Vector3 Position;
        public float RotationX;
        public float RotationY;
        public float RotationZ;
        public float acceleration;
        public float rotation;
        public Quaternion Rotation
        {
            get
            {
                return Quaternion.Euler(RotationX, RotationY, RotationZ);
            }
            set
            {
                RotationX = value.eulerAngles.x;
                RotationY = value.eulerAngles.y;
                RotationZ = value.eulerAngles.z;
            }
        }
        public Vector3 Forward
        {
            get
            {
                return Rotation * Vector3.up;
            }
        }
    }
}
