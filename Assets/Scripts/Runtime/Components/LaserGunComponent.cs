using UnityEngine;

namespace AsteroidsECS
{
    public struct LaserGunComponent
    {
        public IWeaponView weaponView;
        public ITransformableView handler;
        public Vector3 shootingPoint;
        public float maxDuration;
        public float currentCharge;
        public float coolDownTime;
        public bool onShot;
        public bool ready;
    }
}
