using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    public int damage = 1; // How much damage the bullet deals
    public float lifeTime = 5f; // Bullet will self-destroy after this time

    private void Start()
    {
        Destroy(gameObject, lifeTime); // Auto-destroy after a few seconds to prevent buildup
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerController player = collision.GetComponent<PlayerController>();
            if (player != null)
            {
                GameManager.Instance.AddStat(GameManager.StatType.PlayerHealth);
            }

            Destroy(gameObject); // Destroy the projectile after hitting
        }
    }
}