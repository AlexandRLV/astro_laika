using Damage;

namespace LevelObjects.Messages
{
    public struct LevelObjectDestroyedMessage
    {
        public DamageType DamageType;
        public LevelObjectData Data;
    }
}