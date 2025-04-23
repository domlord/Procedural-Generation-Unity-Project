using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAimingAndShooting : MonoBehaviour
{
    public Transform shootPoint; // Where the projectile spawns from
    public GameObject projectilePrefab; // Your projectile prefab
    public float projectileSpeed = 10f; // Speed of projectile

    public Transform aimIndicator;
    public float indicatorDistance = 1.0f; // How far the triangle sits outside the player


    private Camera mainCamera;

    void Start()
    {
        mainCamera = Camera.main;
    }

    void Update()
    {
        AimAtMouse();

        if (Input.GetMouseButtonDown(0)) // Left mouse click
        {
            ShootProjectile();
        }
    }

    void AimAtMouse()
    {
        Vector3 mouseWorldPos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = (mouseWorldPos - transform.position).normalized;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // Optional: Rotate the player
        transform.rotation = Quaternion.Euler(0, 0, angle);

        // Move and rotate the aim indicator based on the direction from the player
        if (aimIndicator != null)
        {
            aimIndicator.position = (Vector2)transform.position + direction * indicatorDistance;
            aimIndicator.rotation = Quaternion.Euler(0, 0, angle);
        }
    }

    void ShootProjectile()
    {
        if (projectilePrefab != null && shootPoint != null)
        {
            GameObject projectile = Instantiate(projectilePrefab, shootPoint.position, shootPoint.rotation);
            Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();

            if (rb != null)
            {
                rb.velocity = shootPoint.right * projectileSpeed;
            }
        }
    }
}