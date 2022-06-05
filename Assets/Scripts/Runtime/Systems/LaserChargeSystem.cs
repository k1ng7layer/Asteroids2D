using Assets.Scripts.Runtime.Components;
using Assets.Scripts.Runtime.UI;
using MyECS2;
using MyUISystem;
using UnityEngine;

namespace Assets.Scripts.Runtime.Systems
{
    public class LaserChargeSystem : IGameSystem
    {
        private EntityFilter<LaserGunComponent, LaserChargeComponent> _filter;
        private UIPresenter _uIPresenter;
        private IIndicator<float> _laserUiIndicator;
        private float _laserCooldown;
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
                gun.ready = false;
                gun.onShot = false;
                gun.currentCharge += Time.deltaTime;
                gun.currentCharge = Mathf.Clamp(gun.currentCharge, 0f, gun.maxDuration);
                gun.weaponView.DisableVizualization();
                gun.weaponView.UpdateAmmoInfo(gun.currentCharge);
                _uIPresenter.PlayerLaserChargeInfo.SetValue(gun.currentCharge);
                _laserCooldown = gun.maxDuration - gun.currentCharge;
                _uIPresenter.PlayerLaserCoolDownInfo.SetValue(_laserCooldown);
                _laserUiIndicator.SetValue(gun.currentCharge);
                if (gun.currentCharge == gun.maxDuration)
                {
                    gun.ready = true;
                    entity.Remove<LaserChargeComponent>();
                }
                    
            }
        }
    }
}
                
