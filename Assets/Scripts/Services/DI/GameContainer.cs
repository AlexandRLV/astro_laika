using System;
using System.Linq;
using System.Reflection;
using Unity.VisualScripting;
using UnityEngine;
using Object = UnityEngine.Object;

namespace DI
{
    public static class GameContainer
    {
        public static Container Common;
        public static Container InGame;

        public static T InstantiateAndResolve<T>(T prefab) where T : MonoBehaviour
        {
            var spawnedObject = Object.Instantiate(prefab);
            var type = typeof(T);
            
            InjectFields(spawnedObject, type);
            InjectMethods(type, spawnedObject);

            return spawnedObject;
        }

        private static void InjectFields(object spawnedObject, Type type)
        {
            var fields = type.GetFields(BindingFlags.NonPublic)
                .Where(x => x.IsDefined(typeof(InjectAttribute)));
            
            foreach (var field in fields)
            {
                var fieldType = field.FieldType;
                if (!TryResolve(fieldType, out object value))
                {
                    throw new ArgumentException(
                        $"Cannot inject type {type.Name}! There's no registration for {fieldType.Name} in both containers!");
                }
                
                field.SetValue(spawnedObject, value);
            }
        }

        private static void InjectMethods(Type type, object spawnedObject)
        {
            var methods = type.GetMethods(BindingFlags.NonPublic);
            foreach (var method in methods)
            {
                var parameters = method.GetParameters();
                var parametersValues = ArrayPool<object>.New(parameters.Length);
                if (parameters.Length > 0)
                {
                    for (int i = 0; i < parameters.Length; i++)
                    {
                        var parameter = parameters[i];
                        var parameterType = parameter.ParameterType;
                        
                        if (!TryResolve(parameterType, out object value))
                        {
                            throw new ArgumentException(
                                $"Cannot inject type {type.Name}! There's no registration for {parameterType.Name} in both containers!");
                        }

                        parametersValues[i] = value;
                    }
                }

                method.Invoke(spawnedObject, parametersValues);
                ArrayPool<object>.Free(parametersValues);
            }
        }

        private static bool TryResolve(Type type, out object value)
        {
            if (Common.CanResolve(type))
            {
                value = Common.Resolve(type);
                return true;
            }
                        
            if (InGame != null && InGame.CanResolve(type))
            {
                value = InGame.Resolve(type);
                return true;
            }

            value = null;
            return false;
        } 
    }
}