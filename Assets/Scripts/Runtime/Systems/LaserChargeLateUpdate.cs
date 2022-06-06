using MyECS2;

namespace AsteroidsECS
{
    public class LaserChargeLateUpdate : IGameSystem
    {
        private EntityFilter<LaserGunComponent, LaserLateChargeComponent> _filter;
        public void Initialize()
        {
            
        }

        public void OnDestroy()
        {
            
        }

        public void Update()
        {   
            foreach (int item in _filter)
            {
                ref var laser = ref _filter.GetFirst(item);
                laser.weaponView.DisableVizualization();
            }
        }
    }
}
