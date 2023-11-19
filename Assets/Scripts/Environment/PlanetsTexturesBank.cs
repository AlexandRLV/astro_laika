using UnityEngine;

namespace Environment
{
    [CreateAssetMenu(fileName = "Planets Textures Bank")]
    public class PlanetsTexturesBank : ScriptableObject
    {
        [SerializeField] public Texture2D[] planets;
    }
}