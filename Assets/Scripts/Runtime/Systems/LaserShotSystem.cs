using Assets.Scripts.Runtime.Components;
using Assets.Scripts.Runtime.UI;
using MyECS2;
using MyUISystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Runtime.Systems
{
    public class LaserShotSystem : IGameSystem
    {

        EntityFilter<LaserGunComponent, ShootComponent> _filter;
        private IIndicator<float> _laserUiIndicator;
        private UIPresenter _uIPresenter;
        public void Initialize()
        {
            _laserUiIndicator = _uIPresenter.FindIndicatorCanvasObject<LaserIndicator>();
        }

        public void OnDestroy()
        {
            
        }

        public void Update()
        {
            foreach (int item in _filter)   
            {
                ref var gun = ref _filter.GetFirst(item);
                ref var entity = ref _filter.GetEntity(item);
                if (gun.currentCharge <= 0f||gun.ready==false)
                {
                    gun.onShot = false;
                    entity.Add<LaserChargeComponent>();
                    entity.Remove<ShootComponent>();
                }
                    
                if (gun.currentCharge > 0f&&gun.ready==true)
                {
                    gun.onShot = true;
                    gun.currentCharge -= Time.deltaTime;
                    gun.currentCharge = Mathf.Clamp(gun.currentCharge, 0f, gun.maxDuration);
                    gun.weaponView.Vizualize();
                    gun.weaponView.UpdateAmmoInfo(gun.currentCharge);
                    _uIPresenter.PlayerLaserChargeInfo.SetValue(gun.currentCharge);
                    _laserUiIndicator.SetValue(gun.currentCharge);
                }
            }

                   
        }
    }
}
                    




              
              
                    
