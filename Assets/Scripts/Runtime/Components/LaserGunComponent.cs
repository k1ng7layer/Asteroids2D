using Assets.Interfaces;
using MyECS2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Runtime.Components
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
