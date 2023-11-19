using UnityEngine;

namespace Environment
{
    [DisallowMultipleComponent]
    public class PlanetRotator : MonoBehaviour
    {
        [SerializeField] private float _speed;

        private void Update()
        {
            float angle = _speed * Time.deltaTime;
            transform.Rotate(Vector3.up, angle);
        }
    }
}