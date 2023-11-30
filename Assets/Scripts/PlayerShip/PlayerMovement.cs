using UnityEngine;

namespace Player
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private float _maxY;
        [SerializeField] private float _minY;
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
            var touchPosition = Camera.main.ScreenToWorldPoint(touch.position);
            MoveToPosition(touchPosition);
        }

        private void UpdateOnStandalone()
        {
            if (!Input.GetMouseButton(0))
                return;
            
            var mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            MoveToPosition(mousePosition);
        }

        private void MoveToPosition(Vector3 position)
        {
            position.x = Mathf.Clamp(position.x, -_maxOffset, _maxOffset);
            position.y = Mathf.Clamp(position.y, _minY, _maxY);
            position.z = 0f;
            transform.position = position;
        }
    }
}