namespace LevelObjects.Messages
{
    public struct LevelObjectDestroyedMessage
    {
        public bool DestroyedByCollision;
        public LevelObjectData Data;
    }
}