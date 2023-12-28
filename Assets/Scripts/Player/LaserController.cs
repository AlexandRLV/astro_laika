using Damage;
using Player.NewPlayer;
using UnityEngine;

namespace Player
{
    public class LaserController : MonoBehaviour
    {
        [Header("Parameters")]
        [SerializeField] private float _damage;
        [SerializeField] private float _reloadTime;
        [SerializeField] private float _laserShowTime;
        [SerializeField] private LayerMask _shootMask;
        
        [Header("References")]
        [SerializeField] private Transform _firePoint;
        [SerializeField] private LineRenderer _lineRenderer;
        [SerializeField] private ParticleSystem _impactEffect;
        [SerializeField] private MovingBorders _borders;

        private bool _powerMode;
        private bool _isShooting;
        private float _shootTimer;

        private GameObject _targetObject;
        private Vector3 _targetPoint;
        private Vector3[] _linePoints;

        private void Start()
        {
            _linePoints = new Vector3[2];
            DeactivateLaser();
        }

        private void Update()
        {
            _shootTimer += Time.deltaTime;
            
            RaycastToTarget();
            if (_targetObject == null)
            {
                UpdateLaserNoTarget();
                return;
            }
            
            if (_isShooting)
            {
                UpdateLaserPositions();
                if (_shootTimer > _laserShowTime)
                {
                    _isShooting = false;
                    DeactivateLaser();
                    return;
                }
            }
            
            if (_shootTimer < _reloadTime)
                return;
            
            _shootTimer = 0f;
            _isShooting = true;
            ApplyDamage();
            ActivateLaser();
            UpdateLaserPositions();
        }

        public void RestoreLaset(float percent)
        {
            float timerRemoveValue = _reloadTime * percent;
            _shootTimer -= timerRemoveValue;
        }

        private void RaycastToTarget()
        {
            bool hitTriggers = Physics2D.queriesHitTriggers;
            Physics2D.queriesHitTriggers = true;
            var hit = Physics2D.Raycast(_firePoint.position, Vector2.up, 10f, _shootMask);
            Physics2D.queriesHitTriggers = hitTriggers;
            
            if (hit.collider == null)
            {
                _targetObject = null;
                return;
            }

            if (hit.point.y > _borders.MaxY)
            {
                _targetObject = null;
                return;
            }
            
            _targetPoint = hit.point;
            _targetObject = hit.collider.gameObject;
        }

        private void UpdateLaserNoTarget()
        {
            if (!_isShooting)
                return;
                
            if (_shootTimer > _laserShowTime)
            {
                _isShooting = false;
                DeactivateLaser();
            }
            else
            {
                _targetPoint = _firePoint.position + Vector3.up * 100f;
                UpdateLaserPositions();
            }
        }

        private void UpdateLaserPositions()
        {
            _linePoints[0] = _firePoint.position;
            _linePoints[1] = _targetPoint;
            _lineRenderer.SetPositions(_linePoints);
            
            _impactEffect.transform.position = _targetPoint;
            _impactEffect.transform.rotation = Quaternion.LookRotation(_firePoint.position);
        }

        private void ApplyDamage()
        {
            var damageable = _targetObject.GetComponentInParent<Damageable>();
            if (damageable != null)
                damageable.Damage(_damage, DamageType.Laser);
        }

        private void ActivateLaser()
        {
            _lineRenderer.enabled = true;
            _impactEffect.Play();
        }
        
        private void DeactivateLaser()
        {
            _lineRenderer.enabled = false;
            _impactEffect.Stop();
        }
    }
}

