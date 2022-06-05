using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.Runtime.Processing
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
