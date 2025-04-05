using System;
using UnityEngine;

namespace AppService.Runtime
{
    public static class GameObjectExtensions
    {
        public static T GetOrAddComponent<T>(this GameObject gameObject) where T : Component
        {
            var component = gameObject.GetComponent<T>();
            if (component == null)
            {
                component = gameObject.AddComponent<T>();
            }
            return component;
        }

        public static T GetComponentSafe<T>(this GameObject gameObject) where T : Component
        {
            var component = gameObject.GetComponent<T>();
            if (component == null)
            {
                throw new ComponentErrorException($"Component not found: {typeof(T).Name}");
            }
            return component;
        }
    }

    public class ComponentErrorException : Exception
    {
        public ComponentErrorException(string message) : base(message)
        {
        }
    }
}