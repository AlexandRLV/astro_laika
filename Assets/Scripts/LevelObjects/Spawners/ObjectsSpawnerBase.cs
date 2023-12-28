using UnityEngine;

namespace LevelObjects
{
    public abstract class ObjectsSpawnerBase : MonoBehaviour
    {
        public abstract void Initialize();
        public abstract void StartSpawn(int count, LevelObjectData data);
        public abstract void StopSpawn();
    }
}