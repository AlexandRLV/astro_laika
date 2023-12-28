using UnityEngine;

namespace Player.NewPlayer
{
    [System.Serializable]
    public class Borders
    {
        public float MinXOffset = 1.5f;
        public float MaxXOffset = 1.5f;
        public float MinYOffset = 1.5f;
        public float MaxYOffset = 1.5f;
        
        [HideInInspector] public float MinX;
        [HideInInspector] public float MaxX;
        [HideInInspector] public float MinY;
        [HideInInspector] public float MaxY;
    }
    
    public class NewPlayerMovement : MonoBehaviour
    {
        [SerializeField] private Borders _borders;
        
        private Camera _mainCamera;

        private void Start()
        {
            _mainCamera = Camera.main;
            ResizeBorders();
        }

        private void Update()
        {
#if UNITY_STANDALONE || UNITY_EDITOR
            if (Input.GetMouseButton(0))
            {
                Vector3 mousePosition = _mainCamera.ScreenToWorldPoint(Input.mousePosition);
                mousePosition.z = transform.position.z;
                transform.position = Vector3.MoveTowards(transform.position, mousePosition, 30 * Time.deltaTime);
            }
#endif

#if UNITY_IOS || UNITY_ANDROID
            if (Input.touchCount == 1)
            {
                Touch touch = Input.touches[0];
                Vector3 touchPosition = _mainCamera.ScreenToWorldPoint(touch.position);
                touchPosition.z = transform.position.z;
                transform.position = Vector3.MoveTowards(transform.position, touchPosition, 30 * Time.deltaTime);
            }
#endif
            
            transform.position = new Vector3
            (
                Mathf.Clamp(transform.position.x, _borders.MinX, _borders.MaxX),
                Mathf.Clamp(transform.position.y, _borders.MinY, _borders.MaxY),
                0
            );
        }

        private void ResizeBorders() 
        {
            _borders.MinX = _mainCamera.ViewportToWorldPoint(Vector2.zero).x + _borders.MinXOffset;
            _borders.MinY = _mainCamera.ViewportToWorldPoint(Vector2.zero).y + _borders.MinYOffset;
            _borders.MaxX = _mainCamera.ViewportToWorldPoint(Vector2.right).x - _borders.MaxXOffset;
            _borders.MaxY = _mainCamera.ViewportToWorldPoint(Vector2.up).y - _borders.MaxYOffset;
        }
    }
}