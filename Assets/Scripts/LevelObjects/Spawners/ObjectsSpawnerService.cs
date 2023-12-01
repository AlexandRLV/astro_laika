using System;
using System.Collections.Generic;
using UnityEngine;

namespace LevelObjects
{
    public class ObjectsSpawnerService : MonoBehaviour
    {
        [Serializable]
        private class SpawnerContainer
        {
            [SerializeField] public LevelObjectType Type;
            [SerializeField] public ObjectsSpawnerBase Spawner;
        }

        [SerializeField] private List<SpawnerContainer> _spawners;

        public ObjectsSpawnerBase GetSpawnerForType(LevelObjectType type)
        {
            foreach (var spawner in _spawners)
            {
                if (spawner.Type == type) return spawner.Spawner;
            }

            return null;
        }
    }
}