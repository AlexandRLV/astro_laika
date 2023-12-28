using UnityEngine;

namespace Levels
{
    [CreateAssetMenu(fileName = "Game Levels Data")]
    public class GameLevelsData : ScriptableObject
    {
        [SerializeField] public LevelInfo[] Levels;
    }
}