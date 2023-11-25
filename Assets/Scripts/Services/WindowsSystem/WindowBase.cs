using DI;
using UnityEngine;

namespace Services.WindowsSystem
{
    public abstract class WindowBase : MonoBehaviour
    {
        [Inject] protected WindowsSystem windowsSystem;
    }
}