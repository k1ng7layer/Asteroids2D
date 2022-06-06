using MyExtensions;
using UnityEngine;

namespace AsteroidsECS
{
    public class PlayerViewFactory : ViewFactory
    {
        public override PlayerView SpawnedPlayerObject { get ; protected set ; }

        public override ITransformableView GetTransformableView()
        {
            GameObject playerObj = Resources.Load<GameObject>("Player/Player1");
            var playerInstance = GameObject.Instantiate(playerObj);
            var viewInstance = playerInstance.transform.GetOrCreateComponent<PlayerView>();
            SpawnedPlayerObject = viewInstance;
            return viewInstance;
        }
    }
}
