using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace MyExtensions
{
    public static class ComponentExtension
    {
        public static T GetOrCreateComponent<T>(this UnityEngine.Component component) where T : Component
        {
            if (!component.gameObject.TryGetComponent<T>(out T requestedComponent))
            {
                requestedComponent = component.gameObject.AddComponent<T>();
            }
            else
            {
                requestedComponent = component.gameObject.GetComponent<T>();
            }
            return requestedComponent;
        }

        public static T GetOrCreateComponentInChildren<T>(this UnityEngine.Component component) where T : Component
        {
            var requestedComponent = component.GetComponentInChildren<T>();
            if (requestedComponent != null)
            {
                return requestedComponent;
            }

            var name = typeof(T).Name;
            GameObject gameObject = new GameObject(name);
            gameObject.transform.SetParent(component.transform);
            requestedComponent = gameObject.AddComponent<T>();
            return requestedComponent;

        }
    }
}
