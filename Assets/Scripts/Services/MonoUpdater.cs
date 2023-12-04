using System;
using UnityEngine;

namespace Services
{
    public class MonoUpdater : MonoBehaviour
    {
        public event Action OnUpdate;

        private void Update()
        {
            OnUpdate?.Invoke();
        }
    }
}