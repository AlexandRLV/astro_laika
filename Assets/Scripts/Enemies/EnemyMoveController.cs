using Damage;
using UnityEngine;

public class EnemyMoveController : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float pointOffset;
    [SerializeField] private bool isStatic = false;

    [SerializeField] private Vector3 movePoint;
    [SerializeField] private bool drawGizmos;

    [SerializeField] private Vector3 _moveZoneCenter;
    [SerializeField] private Vector3 _moveZoneSize;

    [SerializeField] private HealthStatus _healthStatus;
    [SerializeField] private Damageable _enemyDamageable;
    
    private EnemyShipsWaveManager _waveManager;

    public void Init( Vector3 moveZoneCenter, Vector3 moveZoneSize, EnemyShipsWaveManager waveManager)
    {
        _moveZoneCenter = moveZoneCenter;
        _moveZoneSize = moveZoneSize;
        _waveManager = waveManager;
        
        _enemyDamageable.SetStatusCanvas(_healthStatus);
        _enemyDamageable.OnDestroyed += OnDestroyed;

        movePoint = EnemyExtentions.CalculateNewMovePoint(_moveZoneCenter, _moveZoneSize);
    }

    private void Update()
    {
        if (isStatic) return;

        if (Vector3.Distance(transform.position, movePoint) > pointOffset)
        {
            transform.position = Vector3.Lerp(
                transform.position,
                movePoint,
                moveSpeed * Time.deltaTime);
        }
        else
        {
            movePoint = EnemyExtentions.CalculateNewMovePoint(
                _moveZoneCenter, _moveZoneSize);
        }
    }

    private void OnDestroyed(DamageType damageType)
    {
        _waveManager.EnemyDestroyed(gameObject);
        _enemyDamageable.OnDestroyed -= OnDestroyed;
    }

#if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        if (!drawGizmos) return;

        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(movePoint, 0.05f);

        Gizmos.color = new Color(0.8f, 0f, 0f, 0.3f);
        Gizmos.DrawCube(_moveZoneCenter, _moveZoneSize);
    }
#endif
}
