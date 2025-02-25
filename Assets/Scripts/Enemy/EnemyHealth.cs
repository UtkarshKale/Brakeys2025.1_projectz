using UnityEngine;
using UnityEngine.AI;

public class EnemyHealth : MonoBehaviour
{
    public float maxHealth = 100f;
    private float currentHealth;
    private bool isDead = false;

    private Animator animator;
    private NavMeshAgent agent;

    private void Start()
    {
        currentHealth = maxHealth;
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
    }

    


    public void TakeDamage(float damageAmount)
    {
        if (isDead) return;

        currentHealth -= damageAmount;
        Debug.Log(gameObject.name + " took damage: " + damageAmount);

        // Play damage animation if available
        if (animator != null)
        {
            animator.SetTrigger("Hit");
        }

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        isDead = true;
        Debug.Log(gameObject.name + " is dead!");

        // Stop AI movement
        if (agent != null) agent.isStopped = true;

        // Play death animation if available
        if (animator != null)
        {
            animator.SetTrigger("Die");
        }

        // Destroy enemy after animation
        Destroy(gameObject, 2f);
    }
}
