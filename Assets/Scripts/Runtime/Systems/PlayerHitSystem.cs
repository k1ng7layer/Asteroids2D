using MyActionContainer;
using MyECS2;
using UnityEngine;

namespace AsteroidsECS
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
