using UnityEngine;

public class SwordScript : MonoBehaviour
{
    [Header("Attack Settings")]
    public float damage = 25f;
    public float attackCooldown = 0.5f;
    public float luckFactor = 0.2f; // 20% chance for charged attack
    private bool canAttack = true;

    [Header("References")]
    public Animator animator;
    public Collider swordCollider; // Add a collider to the sword (set to trigger)

    private void Start()
    {
        if (swordCollider == null)
        {
            Debug.LogError("Sword Collider not assigned!");
        }
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && canAttack) // Left click to attack
        {
            PerformAttack();
        }
    }

    private void PerformAttack()
    {
        canAttack = false; // Prevents spamming
        bool isChargedAttack = Random.value < luckFactor;

        if (isChargedAttack)
        {
            animator.SetTrigger("JorkingTHATSHIT");
        }
        else
        {
            animator.SetTrigger("SlashReworked");
        }

        // Enable sword collider temporarily
        swordCollider.enabled = true;
        
        Invoke(nameof(ResetAttack), attackCooldown);
    }

    private void ResetAttack()
    {
        canAttack = true;
        swordCollider.enabled = false; // Disable collider after attack
    }

 private void OnTriggerEnter(Collider other)
{
    Debug.Log("Hit detected on:     " + other.gameObject.name);
    
    if (other.CompareTag("Enemy"))
    {
        Debug.Log("Enemy detected!");

        EnemyHealth enemy = other.GetComponent<EnemyHealth>();
        if (enemy != null)
        {
            float finalDamage = Random.value < luckFactor ? enemy.maxHealth : damage;
            enemy.TakeDamage(finalDamage);
            Debug.Log("Damage Dealt: " + finalDamage);
        }
        else
        {
            Debug.Log("EnemyHealth script NOT found on: " + other.gameObject.name);
        }
    }
}



}
