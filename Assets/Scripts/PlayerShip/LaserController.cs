using System.Collections;
using Damage;
using UnityEngine;

namespace Player
{
    public class LaserController : MonoBehaviour
    {
        [SerializeField] private float damagePerSecond;
        [SerializeField] private Transform firePoint;
        [SerializeField] private float reloadTime;
        [SerializeField] private float useTime;

        [SerializeField] private LineRenderer lineRenderer;
        [SerializeField] private ParticleSystem impactEffect;
        [SerializeField] private Vector3 impactEffectOffset;
        [SerializeField] private Light lightEffect;

        private bool _isReloading;
        private bool _seeTarget;
        private bool _usingLaser;
        private bool _powerMode;

        private GameObject _targetObject;
        private Vector3 _targetPoint;

        private Coroutine _shootCoroutine;
        
        private void Update()
        {
            bool hitTriggers = Physics2D.queriesHitTriggers;
            Physics2D.queriesHitTriggers = true;
            var hit = Physics2D.Raycast(firePoint.position, Vector2.up);
            Physics2D.queriesHitTriggers = hitTriggers;
            
            if (hit.collider != null)
            {
                _seeTarget = true;
                _targetPoint = hit.point;
                _targetObject = hit.collider.gameObject;
            }
            else
            {
                _seeTarget = false;
                _targetObject = null;
            }

            ApplyDamage();
            LaserVisualControl();
            
            if (_powerMode) return;
            if (!_isReloading && _seeTarget)
                StartShoot();
        }

        private void StartShoot()
        {
            if (_shootCoroutine != null)
                StopCoroutine(_shootCoroutine);
            
            _shootCoroutine = StartCoroutine(UseLaser(useTime, reloadTime));
        }

        public void RestoreLaset(float percent)
        {
            // TODO
        }

        private IEnumerator UseLaser(float shootTime, float reloadTime)
        {
            _isReloading = false;
            _usingLaser = true;
            
            yield return new WaitForSeconds(shootTime);
            
            _usingLaser = false;
            _isReloading = true;
            
            yield return new WaitForSeconds(reloadTime);
            
            _isReloading = false;
        }

        private void ApplyDamage()
        {
            if (!_usingLaser || !_seeTarget)
                return;
            
            var damageable = _targetObject.GetComponentInParent<Damageable>();
            if (damageable != null)
                damageable.Damage(damagePerSecond * Time.deltaTime, DamageType.Laser);
        }

        private void LaserVisualControl()
        {
            if (_usingLaser && _seeTarget)
            {
                lineRenderer.enabled = true;
                impactEffect.Play();
                lightEffect.enabled = true;

                Vector3[] direction = { firePoint.position, _targetPoint };
                lineRenderer.SetPositions(direction);

                impactEffect.transform.position = _targetPoint - impactEffectOffset;
                impactEffect.transform.rotation = Quaternion.LookRotation(firePoint.position);
            }
            else
            {
                lineRenderer.enabled = false;
                impactEffect.Stop();
                lightEffect.enabled = false;
            }
        }

        private IEnumerator LaserPowerMode()
        {
            // for the future
            yield return null;
        }
    }
}

