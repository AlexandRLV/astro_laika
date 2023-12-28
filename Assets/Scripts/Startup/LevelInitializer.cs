using DI;
using UnityEngine;

namespace Startup
{
    [DefaultExecutionOrder(-1000)]
    public class LevelInitializer : MonoBehaviour
    {
        private static bool _initialized;
        
        [SerializeField] private LevelInitializerBase[] _initializers;

        private void Awake()
        {
            if (_initialized)
            {
                Debug.LogError("Already has initialized level! Destroying initializer...");
                Destroy(gameObject);
                return;
            }

            Initialize();
        }

        private void Initialize()
        {
            GameContainer.InGame = new Container();
            foreach (var initializer in _initializers)
            {
                initializer.Initialize();
            }

            _initialized = true;
        }
    }
}