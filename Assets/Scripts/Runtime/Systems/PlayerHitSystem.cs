using Assets.Scripts.Actions;
using Assets.Scripts.Runtime.Components;
using Assets.Scripts.Runtime.Data;
using MyECS2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Runtime.Systems
{
    public class PlayerHitSystem : IGameSystem
    {
        private EntityFilter<PlayerHitComponent> _filter;
        private PlayerInputData _inputData;
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
                Time.timeScale = 0f;
                _inputData.InputActions.Player.Disable();
                ActionContainer.ResolveAction<GameOverAction>().Dispatch();
            }
                
        }
    }
}
