using DI;
using UnityEngine;

namespace Startup
{
    [DefaultExecutionOrder(-1000)]
    public class LevelInitializer : MonoBehaviour
    {
        private static bool _initialized;
        
        [SerializeField] private InitializerBase[] _initializers;

        private bool _thisInstanceIsInitializer;

        private void Awake()
        {
            if (_initialized)
            {
                Debug.LogError("Already has initialized level! Destroying initializer...");
                Destroy(gameObject);
                return;
            }
            
            GameContainer.InGame = new Container();
            foreach (var initializer in _initializers)
            {
                initializer.Initialize();
            }

            _thisInstanceIsInitializer = true;
            _initialized = true;
        }

        private void OnDestroy()
        {
            if (!_thisInstanceIsInitializer) return;
            
            foreach (var initializer in _initializers)
            {
                initializer.Dispose();
            }
            _initialized = false;
            _thisInstanceIsInitializer = false;
        }
    }
}