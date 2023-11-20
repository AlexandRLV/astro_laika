using Cysharp.Threading.Tasks;

namespace Startup
{
    public abstract class InitializerBase
    {
        public abstract UniTask Initialize();
        public virtual UniTask Dispose() => UniTask.CompletedTask;
    }
}