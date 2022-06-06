using MyECS2;
using MyObjectPool;

namespace AsteroidsECS
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
