using DefaultNamespace;
using System.Collections;
using UnityEngine;

namespace Player
{
    public class LaserController : MonoBehaviour
    {
        [SerializeField] Transform firePoint;
        [SerializeField] Vector3 offset;
        [SerializeField] float reloadTime;
        [SerializeField] float useTime;

        [SerializeField] LineRenderer lineRenderer;
        [SerializeField] ParticleSystem impactEffect;
        [SerializeField] Light lightEffect;

        bool isReloading;
        bool seeTarget;
        bool usingLaser;
        bool powerMode;

        Vector3 target;
        RaycastHit hit;

        private void Update()
        {
            if (Physics.Raycast(firePoint.position, transform.forward, out hit, 100))
            {
                seeTarget = true;
                target = hit.point;
            }
            else seeTarget = false;

            if (powerMode) return;

            if (isReloading == false && seeTarget)
            {
                StartCoroutine(UseLaser(useTime));
                StartCoroutine(LaserReload(reloadTime));
            }

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

        private void LaserVisualControl()
        {
            if (usingLaser && seeTarget)
            {
                lineRenderer.enabled = true;
                impactEffect.Play();
                lightEffect.enabled = true;

                Vector3[] direction = { firePoint.position, target };
                lineRenderer.SetPositions(direction);

                impactEffect.transform.position = hit.point - offset;
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

