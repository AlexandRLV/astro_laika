using UnityEngine;

public enum EnemyWeaponType
{
    Rocket,
    Bullets,
    Laser
}

public class EnemyController : MonoBehaviour
{
    [SerializeField] EnemyWeaponType _enemyWeaponType;

    [SerializeField] EnemyMoveController _enemyMoveController;

    private void Awake()
    {
        
    }
}
