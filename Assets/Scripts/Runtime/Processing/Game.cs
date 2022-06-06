using UnityEngine;
using UnityEngine.SceneManagement;

namespace AsteroidsECS
{
    public static class Game
    {
        private static Scene _currentScene;
        public static void LoadScene(int sceneID)
        {
            AsyncOperation operation = SceneManager.LoadSceneAsync(sceneID);
            operation.completed += HandleSceneLoaded;
        }
        private static void HandleSceneLoaded(AsyncOperation action)
        {
            if (action.isDone)
            {
                _currentScene = SceneManager.GetActiveScene();
            }

        }
    }
}
