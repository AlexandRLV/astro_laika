using LevelObjects;
using UnityEngine;

public class EnemyShootController : MonoBehaviour
{
    [SerializeField] private float _reloadTime;
    [SerializeField] private Transform _firePoint;
    [SerializeField] private EnemyShell _shellPrefab;

    private LevelObjectData _enemyData;
    
    private bool _canShoot = false;
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
        if (!_canShoot) UpdateReloadTimer();
        else
        {
            EnemyShell shell = Instantiate(_shellPrefab, _firePoint.position, _firePoint.rotation);

            _canShoot = false;
            _reloadTimer = _reloadTime;

        }


    }

    private void UpdateReloadTimer()
    {
        _reloadTimer -= Time.deltaTime;
        if (_reloadTimer <= 0)
        {
            _canShoot = true;
            return;
        }
    }

}
