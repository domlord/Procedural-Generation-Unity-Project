using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float lifeTime = 3f;

    void Start()
    {
        // Destroy bullet after a few seconds to clean up
        Destroy(gameObject, lifeTime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            other.gameObject.GetComponent<EnemyController>().Death();
            Destroy(gameObject); // Destroy the bullet too
        }
        // else if (other.gameObject.layer == LayerMask.NameToLayer("Walls"))
        // {
        //     Destroy(gameObject); // Optional: destroy bullet on walls
        // }
    }
}