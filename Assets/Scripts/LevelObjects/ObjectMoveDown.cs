using DefaultNamespace;
using UnityEngine;

namespace LevelObjects
{
    public class ObjectMoveDown : MonoBehaviour
    {
        [SerializeField] private float _speed;

        private void Update()
        {
            transform.AddYPosition(-_speed * Time.deltaTime);
        }
    }
}