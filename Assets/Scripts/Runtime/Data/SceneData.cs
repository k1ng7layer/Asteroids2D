using UnityEngine;

namespace AsteroidsECS
{
    public class SceneData
    {
        public SceneData(Camera mainCamera)
        {
            MainCamera = mainCamera;
        }
        public Camera MainCamera { get; private set; }
    }
}
