using System.Collections;
using Damage;
using UnityEngine;

namespace Player
{
    public class LaserController : MonoBehaviour
    {
        [Header("Parameters")]
        [SerializeField] private float _damage;
        [SerializeField] private float _reloadTime;
        [SerializeField] private float _laserShowTime;
        
        [Header("References")]
        [SerializeField] private Transform _firePoint;
        [SerializeField] private LineRenderer _lineRenderer;
        [SerializeField] private ParticleSystem _impactEffect;
        [SerializeField] private Vector3 _impactEffectOffset;

        private bool _powerMode;
        private bool _isShooting;
        private float _shootTimer;

        private GameObject _targetObject;
        private Vector3 _targetPoint;
        private Vector3[] _linePoints;

        private Coroutine _shootCoroutine;

        private void Start()
        {
            _linePoints = new Vector3[2];
            DeactivateLaser();
        }

        private void Update()
        {
            _shootTimer -= Time.deltaTime;
            if (_shootTimer > 0f && !_powerMode)
                return;
            
            bool hitTriggers = Physics2D.queriesHitTriggers;
            Physics2D.queriesHitTriggers = true;
            var hit = Physics2D.Raycast(_firePoint.position, Vector2.up);
            Physics2D.queriesHitTriggers = hitTriggers;
            
            if (hit.collider == null)
            {
                _targetObject = null;
                return;
            }
            
            _targetPoint = hit.point;
            _targetObject = hit.collider.gameObject;
            _shootTimer = _reloadTime;
            
            ApplyDamage();
            StartShoot();
        }

        private void StartShoot()
        {
            if (_shootCoroutine != null)
                StopCoroutine(_shootCoroutine);
            
            _shootCoroutine = StartCoroutine(UseLaser(_laserShowTime));
        }

        public void RestoreLaset(float percent)
        {
            float timerRemoveValue = _reloadTime * percent;
            _shootTimer -= timerRemoveValue;
        }

        private IEnumerator UseLaser(float shootTime)
        {
            _lineRenderer.enabled = true;
            _impactEffect.Play();

            _linePoints[0] = _firePoint.position;
            _linePoints[1] = _targetPoint;
            _lineRenderer.SetPositions(_linePoints);

            _impactEffect.transform.position = _targetPoint - _impactEffectOffset;
            _impactEffect.transform.rotation = Quaternion.LookRotation(_firePoint.position);

            float timer = 0f;
            while (timer < shootTime)
            {
                timer += Time.deltaTime;
                _linePoints[0] = _firePoint.position;
                _linePoints[1] = _targetPoint;
                _lineRenderer.SetPositions(_linePoints);
                yield return null;
            }
            
            DeactivateLaser();
        }

        private void ApplyDamage()
        {
            Debug.Log($"Applying {_damage} damage");
            var damageable = _targetObject.GetComponentInParent<Damageable>();
            if (damageable != null)
                damageable.Damage(_damage, DamageType.Laser);
        }
        
        private void DeactivateLaser()
        {
            _lineRenderer.enabled = false;
            _impactEffect.Stop();
        }
    }
}

