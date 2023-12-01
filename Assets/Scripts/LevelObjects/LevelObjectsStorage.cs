using UnityEngine;

namespace LevelObjects
{
    [CreateAssetMenu(fileName = "Level Objects Storage")]
    public class LevelObjectsStorage : ScriptableObject
    {
        public LevelObjectData[] LevelObjects;

        public LevelObjectData FindDataForType(LevelObjectType type)
        {
            foreach (var levelObject in LevelObjects)
            {
                if (levelObject.Type == type)
                    return levelObject;
            }

            return null;
        }
    }
}