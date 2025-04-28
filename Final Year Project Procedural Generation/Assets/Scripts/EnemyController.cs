using System.Collections;
using UnityEngine;

public enum EnemyState
{
    Idle,
    Patrol,
    Chase,
    Dead,
    Attack
}

public class EnemyController : MonoBehaviour
{
    public EnemyState enemyState = EnemyState.Patrol;
    public float detectionRadius = 5f;
    public float movementSpeed = 2f;
    public float attackRadius = 1.5f;
    public float attackCooldownTime = 1f;

    private GameObject playerTarget;
    private bool isCooldownActive;
    private bool isChoosingDirection;
    public bool isOutsideRoom;

    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] float projectileSpeed = 20f;

    private void Start()
    {
        playerTarget = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        HandleStateMachine();
    }


    private void HandleStateMachine()
    {
        if (isOutsideRoom)
        {
            enemyState = EnemyState.Idle;
            return;
        }

        if (enemyState == EnemyState.Dead)
            return;

        float playerDistance = Vector3.Distance(transform.position, playerTarget.transform.position);

        if (playerDistance <= attackRadius)
        {
            enemyState = EnemyState.Attack;
        }
        else if (playerDistance <= detectionRadius)
        {
            enemyState = EnemyState.Chase;
        }
        else
        {
            enemyState = EnemyState.Patrol;
        }

        switch (enemyState)
        {
            case EnemyState.Patrol:
                Patrol();
                break;
            case EnemyState.Chase:
                ChasePlayer();
                break;
            case EnemyState.Attack:
                ChasePlayer(); // 👈 Move toward player
                PerformAttack();
                break;
        }
    }

    private void Patrol()
    {
        if (!isChoosingDirection)
        {
            StartCoroutine(ChooseNewDirection());
        }

        transform.position += transform.up * (movementSpeed * Time.deltaTime);
    }

    private void ChasePlayer()
    {
        var direction = (playerTarget.transform.position - transform.position).normalized;
        transform.position += direction * (movementSpeed * Time.deltaTime);
    }

    private void PerformAttack()
    {
        if (isCooldownActive)
            return;

        Debug.Log("Enemy is shooting");
        // ShootAtPlayer();

        StartCoroutine(AttackCooldown());
    }


    private IEnumerator ChooseNewDirection()
    {
        isChoosingDirection = true;
        yield return new WaitForSeconds(Random.Range(2f, 5f));

        float randomAngle = Random.Range(0f, 360f);
        transform.rotation = Quaternion.Euler(0, 0, randomAngle);

        isChoosingDirection = false;
    }

    private IEnumerator AttackCooldown()
    {
        isCooldownActive = true;
        yield return new WaitForSeconds(attackCooldownTime);
        isCooldownActive = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerBullet"))
        {
            HandleDeath();
        }
    }

    public void HandleDeath()
    {
        Destroy(gameObject);
    }

    // private void ShootAtPlayer()
    // {
    //     if (projectilePrefab == null)
    //     {
    //         Debug.LogWarning($"Enemy {name} is missing a projectile prefab!");
    //         return;
    //     }
    //
    //     GameObject projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
    //
    //     Vector2 direction = (playerTarget.transform.position - transform.position).normalized;
    //     Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
    //
    //     if (rb != null)
    //     {
    //         rb.velocity = direction * projectileSpeed;
    //     }
    // }
}