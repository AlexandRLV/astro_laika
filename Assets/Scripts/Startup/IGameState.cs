using Cysharp.Threading.Tasks;

namespace Startup
{
    public interface IGameState
    {
        public UniTask OnEnter();
        public UniTask OnExit();
    }
}