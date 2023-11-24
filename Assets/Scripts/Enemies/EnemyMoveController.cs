using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMoveController : MonoBehaviour
{
    [SerializeField] float moveSpeed;
    [SerializeField] float pointOffset;
    [SerializeField] bool isStatic = false;

    [SerializeField] Vector3 movePoint;
    [SerializeField] bool drawGizmos;

    [SerializeField] Vector3 moveZoneCenter, moveZoneSize;
    EnemyShipsWaveManager waveManager;

    public void Init( Vector3 moveZoneCenter, Vector3 moveZoneSize, EnemyShipsWaveManager waveManager)
    {
        this.moveZoneCenter = moveZoneCenter;
        this.moveZoneSize = moveZoneSize;
        this.waveManager = waveManager;

        movePoint = EnemyExtentions.CalculateNewMovePoint(
            this.moveZoneCenter, this.moveZoneSize);
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
                moveZoneCenter, moveZoneSize);
        }
    }

#if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        if (!drawGizmos) return;

        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(movePoint, 0.05f);

        Gizmos.color = new Color(0.8f, 0f, 0f, 0.3f);
        Gizmos.DrawCube(moveZoneCenter, moveZoneSize);
    }
#endif

    private void OnDestroy()
    {
        waveManager.ReduceAliveEnemies();
    }

}
