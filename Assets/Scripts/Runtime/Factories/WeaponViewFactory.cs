using Assets.Interfaces;
using Assets.Scripts.Runtime.Views;
using MyExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Runtime.Factories
{
    public enum WeaponType
    {
        FIRE_GUN,
        LASER_GUN,
    }
    public class WeaponViewFactory
    {
        private PlayerView _targetPlayer;
        public WeaponViewFactory(PlayerView targetPlayer)
        {
            _targetPlayer = targetPlayer;
        }
        public IWeaponView CreateWeaponView(WeaponType weaponType)
        {
            switch (weaponType)
            {
                case WeaponType.FIRE_GUN:
                    IWeaponView fireGunView = CreateWeaponObject<FireGunView>();
                    return fireGunView;
                case WeaponType.LASER_GUN:
                    IWeaponView laserGunView = CreateWeaponObject<LaserGunView>();
                    return laserGunView;
                default:
                    return null;
            }
        }

        private T CreateWeaponObject<T>() where T : MonoBehaviour
        {
            var obj = _targetPlayer.transform.GetOrCreateComponentInChildren<T>();

            return obj;
        }
    }
}