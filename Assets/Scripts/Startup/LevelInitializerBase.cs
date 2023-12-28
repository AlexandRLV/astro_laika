using UnityEngine;

namespace Startup
{
    public abstract class LevelInitializerBase : MonoBehaviour
    {
        public abstract void Initialize();
        public virtual void Dispose() { }
    }
}