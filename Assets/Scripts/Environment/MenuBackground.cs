using UnityEngine;

namespace Environment
{
    public class MenuBackground : MonoBehaviour
    {
        public bool Active
        {
            get => gameObject.activeSelf;
            set => gameObject.SetActive(value);
        }
    }
}