using Assets.Scripts.Configuration;
using MyECS2;

namespace Assets.Scripts.Runtime
{
    public class SystemRunner
    {
        private SystemHandler _systemHandler;
        private SystemHandler _lateUpdateHandler;

        public SystemRunner(RootConfiguration root,SystemConfiguration systemConfiguration)
        {
            _systemHandler = root.ECS.GetSystemHandler(systemConfiguration);
            _lateUpdateHandler = root.ECS.GetLateUpdateSystems(systemConfiguration);
        }

        public void Init()
        {
            _systemHandler.Initialize();
            _lateUpdateHandler.Initialize();
        }

        public void Update()
        {
            _systemHandler.Update();
        }

        public void LateUpdate()
        {
            _lateUpdateHandler.Update();
        }

        public void Destroy()
        {
            _systemHandler.Destroy();
            _lateUpdateHandler.Destroy();
        }
    }
}

