using Assets.Scripts.Runtime.Components;
using MyECS2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Runtime.Systems
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
