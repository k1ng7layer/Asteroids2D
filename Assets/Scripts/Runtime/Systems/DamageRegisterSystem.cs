using Assets.Scripts.Runtime.Components;
using Assets.Scripts.Runtime.Data;
using MyECS2;
using MyObjectPool;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Runtime.Systems
{
    public class DamageRegisterSystem : IGameSystem
    {
        private EntityFilter<EnemyComponent, HitComponent> _filter;
        private EnemySpawnData _enemySpawnData;
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
                PlayerSceneData.Instance.PlayerScore++;
                ref var enemyComp = ref _filter.GetFirst(item);
                ref var entity = ref _filter.GetEntity(item);
                ObjectPoolFacade.ReturnObjectToPool(enemyComp.attachedView);
                var spawnedCount = _enemySpawnData.spawnedCount[enemyComp.enemyType];

                _enemySpawnData.spawnedCount[enemyComp.enemyType] = --spawnedCount;
                entity.Destroy();
               
               
            }
        }
    }
}
