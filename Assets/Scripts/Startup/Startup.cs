using Cysharp.Threading.Tasks;
using DI;
using Startup.Common;
using UnityEngine;

namespace Startup
{
    public class Startup : MonoBehaviour
    {
        private InitializerBase[] _commonInitializers = {
            new ServicesInitializer(),
            new UiInitializer(),
        };
        
        private void Awake()
        {
            Initialize().Forget();
        }

        private async UniTask Initialize()
        {
            GameContainer.Common = new Container();
            foreach (var initializer in _commonInitializers)
            {
                await initializer.Initialize();
            }
        }
    }
}