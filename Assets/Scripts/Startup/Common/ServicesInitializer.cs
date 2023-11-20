using Cysharp.Threading.Tasks;
using DI;
using Services;

namespace Startup.Common
{
    public class ServicesInitializer : InitializerBase
    {
        public override UniTask Initialize()
        {
            var messageBroker = new MessageBroker();
            GameContainer.Common.Register(messageBroker);
            
            return UniTask.CompletedTask;
        }
    }
}