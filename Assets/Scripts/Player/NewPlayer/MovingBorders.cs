using UnityEngine;

namespace Player.NewPlayer
{
    [CreateAssetMenu]
    public class MovingBorders : ScriptableObject
    {
        public float MinXOffset = 1.5f;
        public float MaxXOffset = 1.5f;
        public float MinYOffset = 1.5f;
        public float MaxYOffset = 1.5f;
        
        [HideInInspector] public float MinX;
        [HideInInspector] public float MaxX;
        [HideInInspector] public float MinY;
        [HideInInspector] public float MaxY;
    }
}