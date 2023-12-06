using UnityEngine;

namespace Environment
{
    [DisallowMultipleComponent]
    public class Rotator : MonoBehaviour
    {
        [SerializeField] private Axis _rotateAxis;
        [SerializeField] private bool _randomRotation;
        [SerializeField] private float _speed;

        private Vector3 _rotateVector;
        
        private void Start()
        {
            UpdateRotateVector();
            
            float angle = Random.Range(0f, 360f);
            transform.Rotate(_rotateVector, angle);
        }

        private void Update()
        {
#if UNITY_EDITOR
            if (!_randomRotation)
                UpdateRotateVector();
#endif
            
            float angle = _speed * Time.deltaTime;
            transform.Rotate(_rotateVector, angle);
        }

        private void UpdateRotateVector()
        {
            if (_randomRotation)
            {
                _rotateVector = new Vector3(
                    Random.Range(-1f, 1f),
                    Random.Range(-1f, 1f),
                    Random.Range(-1f, 1f))
                    .normalized;
                
                if (_rotateVector == Vector3.zero)
                    _rotateVector = Vector3.up;
                
                return;
            }
            
            _rotateVector = Vector3.zero;
            if ((_rotateAxis & Axis.X) == Axis.X)
                _rotateVector += Vector3.right;
            if ((_rotateAxis & Axis.Y) == Axis.Y)
                _rotateVector += Vector3.up;
            if ((_rotateAxis & Axis.Z) == Axis.Z)
                _rotateVector += Vector3.forward;
        }
    }
}