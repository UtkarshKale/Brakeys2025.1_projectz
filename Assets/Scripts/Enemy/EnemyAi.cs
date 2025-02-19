using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public Transform player;
    public GameObject bulletPrefab;
    public Transform shootPoint;
    public float fireRate = 1f;
    public float shootingRange = 10f;
    public float detectionRange = 20f;
    public float stoppingDistance = 5f;

    private NavMeshAgent agent;
    private bool canSeePlayer = false;
    private bool isShooting = false;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        // Start shooting loop (will only shoot if player is in range)
        InvokeRepeating(nameof(Shoot), 0f, 1f / fireRate);
    }

    private void Update()
    {
        if (player == null) return;

        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        // Check if player is within detection range
        if (distanceToPlayer <= detectionRange)
        {
            canSeePlayer = CheckLineOfSight();

            if (canSeePlayer)
            {
                agent.SetDestination(player.position);

                // Stop moving if close enough
                if (distanceToPlayer <= stoppingDistance)
                {
                    agent.isStopped = true;
                }
                else
                {
                    agent.isStopped = false;
                }
            }
        }
        else
        {
            agent.isStopped = true;
            canSeePlayer = false;
        }
    }

    private bool CheckLineOfSight()
    {
        RaycastHit hit;
        Vector3 directionToPlayer = (player.position - transform.position).normalized;

        if (Physics.Raycast(transform.position + Vector3.up, directionToPlayer, out hit, detectionRange))
        {
            if (hit.collider.CompareTag("Player"))
            {
                return true;
            }
        }
        return false;
    }

    private void Shoot()
    {
        if (canSeePlayer && Vector3.Distance(transform.position, player.position) <= shootingRange)
        {
            transform.LookAt(new Vector3(player.position.x, transform.position.y, player.position.z));

            GameObject bullet = Instantiate(bulletPrefab, shootPoint.position, shootPoint.rotation);
            Bullet bulletScript = bullet.GetComponent<Bullet>();

            if (bulletScript != null)
            {
                bulletScript.shooterTag = gameObject.tag; // Prevent self-damage
            }
        }
    }

      private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, shootingRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
    }
}
