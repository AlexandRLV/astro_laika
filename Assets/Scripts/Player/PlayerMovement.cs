using DefaultNamespace;
using UnityEngine;

namespace Player
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private float _maxY;
        [SerializeField] private float _maxOffset;
        
        private void Update()
        {
            if (!Application.isFocused)
                return;
            
#if UNITY_EDITOR || UNITY_STANDALONE
            UpdateOnStandalone();
#elif UNITY_ANDROID || UNITY_IOS
            UpdateOnMobile();
#endif
        }

        private void UpdateOnMobile()
        {
            if (Input.touchCount == 0) return;

            var touch = Input.GetTouch(0);
            if (touch.position.y > _maxY) return;
            
            MoveToPosition(touch.position.x);
        }

        private void UpdateOnStandalone()
        {
            var mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if (mousePosition.y > _maxY) return;
            
            MoveToPosition(mousePosition.x);
        }

        private void MoveToPosition(float x)
        {
            x = Mathf.Clamp(x, -_maxOffset, _maxOffset);
            transform.SetXPosition(x);
        }
    }
}