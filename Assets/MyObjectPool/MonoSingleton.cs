using UnityEngine;

namespace MyObjectPool
{
    public class MonoSingleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        private static T _instance;
        public static T Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = FindObjectOfType<T>();
                    if (_instance == null)
                    {
                        GameObject gameObject = new GameObject();
                        gameObject.name = typeof(T).ToString();
                        _instance = gameObject.AddComponent<T>();
                    }
                }
                return _instance;
            }
        }
    }
}
