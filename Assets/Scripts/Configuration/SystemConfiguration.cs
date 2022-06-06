using MyECS2;
using UnityEngine;

namespace AsteroidsECS
{
    public class SystemConfiguration
    {
        public SystemConfiguration(RootConfiguration root, UIPresenter uIPresenter, GameObject asteroidsSpawnPoint, GameObject ufosSpawnPoint, EntityHandler entityHandler)
        {
            Root = root;
            UI = uIPresenter;
            AsteroidsSpawnPoint = asteroidsSpawnPoint;
            UfosSpawnPoint = ufosSpawnPoint;
            EntityHandler = entityHandler;
        }
        public RootConfiguration Root { get; private set; }
        public EntityHandler EntityHandler { get; private set; }
        public UIPresenter UI { get; private set; }
        public GameObject AsteroidsSpawnPoint;
        public GameObject UfosSpawnPoint;
    }
}
