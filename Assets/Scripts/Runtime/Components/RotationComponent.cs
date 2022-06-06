using UnityEngine;

namespace AsteroidsECS
{
    public struct RotationComponent
    {
        public float RotationX;
        public float RotationY;
        public float RotationZ;
        public float angleDelta;
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
