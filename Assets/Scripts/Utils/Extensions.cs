using UnityEngine;

namespace DefaultNamespace
{
    public static class Extensions
    {
        public static Vector3 WithX(this Vector3 value, float x)
        {
            value.x = x;
            return value;
        }
        
        public static Vector3 AddY(this Vector3 value, float y)
        {
            value.y += y;
            return value;
        }

        public static void SetXPosition(this Transform transform, float x)
        {
            transform.position = transform.position.WithX(x);
        }

        public static void AddYPosition(this Transform transform, float y)
        {
            transform.position = transform.position.AddY(y);
        }

        public static T GetRandom<T>(this T[] array)
        {
            if (array.Length == 0) return default;
            return array[Random.Range(0, array.Length)];
        }
    }
}