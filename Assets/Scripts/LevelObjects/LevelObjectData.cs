using System;
using UnityEngine;

namespace LevelObjects
{
    [Serializable]
    public class LevelObjectData
    {
        [Header("Object type")]
        [SerializeField] public LevelObjectType Type;
        
        [Header("Object destroy")]
        [SerializeField] public int ScoresForDestroy;
        [SerializeField] public int ShotsToDestroy;
        [SerializeField] [Range(0f, 1f)] public float LaserPercentBonusForDestroy;
        [SerializeField] [Range(0f, 1f)] public float ShieldPercentBonusForDestroy;
        
        [Header("Collision")]
        [SerializeField] [Range(0f, 1f)] public float CollisionToPlayerDamagePercent;
        [SerializeField] public int ScoresPenaltyOnCollision;

        [Header("Shoot damage, only for shooting objects")]
        [SerializeField] [Range(0f, 1f)] public float ShootDamagePercent;
    }
}