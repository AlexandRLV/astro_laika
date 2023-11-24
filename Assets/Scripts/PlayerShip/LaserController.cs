using DefaultNamespace;
using System.Collections;
using UnityEngine;

namespace Player
{
    public class LaserController : MonoBehaviour
    {
        [SerializeField] float damagePerSecond;
        [SerializeField] Transform firePoint;
        [SerializeField] float reloadTime;
        [SerializeField] float useTime;

        [SerializeField] LineRenderer lineRenderer;
        [SerializeField] ParticleSystem impactEffect;
        [SerializeField] Vector3 impactEffectOffset;
        [SerializeField] Light lightEffect;

        bool isReloading;
        bool seeTarget;
        bool usingLaser;
        bool powerMode;

        GameObject targetObject;
        Vector3 targetPoint;
        RaycastHit hit;

        private void Update()
        {
            if (Physics.Raycast(firePoint.position, transform.forward, out hit, 100))
            {
                seeTarget = true;
                targetPoint = hit.point;
                targetObject = hit.collider.gameObject;
            }
            else
            {
                seeTarget = false;
                targetObject = null;
            }

            if (powerMode) return;

            if (isReloading == false && seeTarget)
            {
                StartCoroutine(UseLaser(useTime));
                StartCoroutine(LaserReload(reloadTime));
            }

            ApplyDamage();
            LaserVisualControl();
        }

        private IEnumerator UseLaser(float time)
        {
            usingLaser = true;
            yield return new WaitForSeconds(time);
            usingLaser = false;
        }

        private IEnumerator LaserReload(float time)
        {
            isReloading = true;
            yield return new WaitForSeconds(time);
            isReloading = false;
        }

        private void ApplyDamage()
        {
            if (usingLaser && targetObject != null)
            {
                Damageable damageable = targetObject.GetComponentInParent<Damageable>();

                if (damageable) damageable.GetDamage(damagePerSecond * Time.deltaTime);

            }
        }

        private void LaserVisualControl()
        {
            if (usingLaser && seeTarget)
            {
                lineRenderer.enabled = true;
                impactEffect.Play();
                lightEffect.enabled = true;

                Vector3[] direction = { firePoint.position, targetPoint };
                lineRenderer.SetPositions(direction);

                impactEffect.transform.position = hit.point - impactEffectOffset;
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

