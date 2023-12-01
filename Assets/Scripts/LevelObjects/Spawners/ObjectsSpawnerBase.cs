using UnityEngine;

namespace LevelObjects
{
    public abstract class ObjectsSpawnerBase : MonoBehaviour
    {
        public abstract void StartSpawn(int count);
        public abstract void StopSpawn();
    }
}