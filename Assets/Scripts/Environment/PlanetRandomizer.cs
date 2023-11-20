using DefaultNamespace;
using UnityEngine;

namespace Environment
{
    [RequireComponent(typeof(Renderer))]
    [DisallowMultipleComponent]
    public class PlanetRandomizer : MonoBehaviour
    {
        [SerializeField] private PlanetsTexturesBank _bank;

        private void Start()
        {
            var renderer = GetComponent<Renderer>();
            renderer.material.SetTexture("_BaseMap", _bank.planets.GetRandom());
        }
    }
}