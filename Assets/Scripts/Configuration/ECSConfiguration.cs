using MyECS2;
using UnityEngine;

namespace Assets.Scripts.Configuration
{

    public abstract class ECSConfiguration:ScriptableObject
    {
        protected SystemHandler _systemHandler;
        public abstract SystemHandler GetSystemHandler(SystemConfiguration systemConfiguration);
        public abstract SystemHandler GetLateUpdateSystems(SystemConfiguration systemConfiguration);
    }
}


        
          

