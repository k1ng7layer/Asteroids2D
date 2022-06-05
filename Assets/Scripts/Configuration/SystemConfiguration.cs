using Assets.Scripts.Runtime.UI;
using MyECS2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Configuration
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
