using LevelObjects;
using UnityEngine;

namespace Enemies
{
    public class EnemyShootController : MonoBehaviour
    {
        [SerializeField] private float _reloadTime;
        [SerializeField] private Transform _firePoint;
        [SerializeField] private EnemyShell _shellPrefab;

        private LevelObjectData _enemyData;
    
        private bool _canShoot;
        private float _reloadTimer;

        public void Initialize(LevelObjectData data)
        {
            _enemyData = data;
        }

        private void Start()
        {
            _reloadTimer = _reloadTime;
        }

        private void Update()
        {
            if (!_canShoot)
            {
                UpdateReloadTimer();
            }
            else
            {
                var shell = Instantiate(_shellPrefab, _firePoint.position, _firePoint.rotation);
                shell.DamagePercent = _enemyData.ShootDamagePercent;

                _canShoot = false;
                _reloadTimer = _reloadTime;
            }
        }

        private void UpdateReloadTimer()
        {
            _reloadTimer -= Time.deltaTime;
            if (_reloadTimer <= 0) _canShoot = true;
        }
    }
}
