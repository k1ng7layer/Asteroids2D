using UnityEngine;

namespace AsteroidsECS
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
