using UnityEngine;

public class TestSpawner : MonoBehaviour
{
    [SerializeField] private Vector2 spawnZoneMin = new Vector2(-5, 10);
    [SerializeField] private Vector2 spawnZoneMax = new Vector2(5, 15);

    private void OnDrawGizmos()
    {
        // Visualize the spawn zone
        Gizmos.color = Color.blue;
        Vector3 bottomLeft = new Vector3(spawnZoneMin.x, spawnZoneMin.y, 0);
        Vector3 topRight = new Vector3(spawnZoneMax.x, spawnZoneMax.y, 0);

        Gizmos.DrawLine(bottomLeft, new Vector3(topRight.x, bottomLeft.y, 0));
        Gizmos.DrawLine(bottomLeft, new Vector3(bottomLeft.x, topRight.y, 0));
        Gizmos.DrawLine(topRight, new Vector3(topRight.x, bottomLeft.y, 0));
        Gizmos.DrawLine(topRight, new Vector3(bottomLeft.x, topRight.y, 0));
    }

}
