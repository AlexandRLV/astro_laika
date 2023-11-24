using UnityEngine;

public static class EnemyExtentions
{
    public static Vector3 CalculateNewMovePoint(Vector3 center, Vector3 size)
    {
        float newX = Random.Range(center.x - size.x / 2f, center.x + size.x / 2f);
        float newY = Random.Range(center.y - size.y / 2f, center.y + size.y / 2f);

        Vector3 newPoint = new Vector3(newX, newY, 0);

        return newPoint;
    }
}
