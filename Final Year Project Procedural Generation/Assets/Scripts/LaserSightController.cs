using UnityEngine;

public class LaserSightController : MonoBehaviour
{
    public LineRenderer laserLine;
    public float maxLaserDistance = 10f;
    public LayerMask laserCollisionMask;

    private Camera mainCamera;

    void Start()
    {
        mainCamera = Camera.main;
    }

    void Update()
    {
        AimLaser();
    }

    void AimLaser()
    {
        Vector3 mouseWorldPos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        Vector2 aimDirection = (mouseWorldPos - transform.position).normalized;

        // Start point of the laser = player position
        Vector3 start = transform.position;
        Vector3 end = start + (Vector3)(aimDirection * maxLaserDistance);

        // Optional: check for collisions with walls, enemies, etc.
        RaycastHit2D hit = Physics2D.Raycast(start, aimDirection, maxLaserDistance, laserCollisionMask);
        if (hit.collider != null)
        {
            end = hit.point; // Laser stops at collision
        }

        // Draw the laser
        laserLine.SetPosition(0, start);
        laserLine.SetPosition(1, end);
    }
}