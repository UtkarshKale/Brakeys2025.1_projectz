using UnityEngine;
using System.Collections;

public class SwordManager : MonoBehaviour
{
    [Header("Attack Settings")]
    public float damageAmount = 25f;
    public float attackCooldown = 1f; // Cooldown between attacks
    public float attackDuration = 0.3f; // Time before disabling the attack collider

    [Header("VFX Settings")]
    public GameObject attackVFX; // Reference to the VFX GameObject
    public Transform vfxSpawnPoint; // Empty GameObject for VFX positioning
    public float vfxDuration = 0.4f; // Time before VFX disappears

    [Header("References")]
    public Collider attackCollider; // Attack hitbox collider
    public Animator animator; // Animator for attack animations

    private bool isAttacking = false;
    private float lastAttackTime;

    void Update()
    {
        if (Input.GetButtonDown("Fire1") && !isAttacking && Time.time >= lastAttackTime + attackCooldown)
        {
            StartCoroutine(PerformAttack());
        }
    }

    private IEnumerator PerformAttack()
    {
        isAttacking = true;
        lastAttackTime = Time.time;

        // Play a random attack animation
        string attackAnimation = Random.value > 0.5f ? "Attack_Horizontal" : "Attack_Vertical";
        animator.Play(attackAnimation);

        // Enable attack collider
        attackCollider.enabled = true;

        // Activate the VFX at the predefined empty game object's position
        if (attackVFX != null)
        {
            attackVFX.SetActive(true);
            StartCoroutine(DisableVFXAfterDelay(vfxDuration)); // Schedule VFX deactivation
        }

        // Wait for attack impact
        yield return new WaitForSeconds(attackDuration);

        // Disable attack collider
        attackCollider.enabled = false;

        // Wait for attack cooldown before allowing another attack
        yield return new WaitForSeconds(attackCooldown - attackDuration);
        isAttacking = false;
    }

    private IEnumerator DisableVFXAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        if (attackVFX != null)
        {
            attackVFX.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (attackCollider.enabled && isAttacking)
        {
            if (other.CompareTag("Enemy") || other.gameObject.layer == LayerMask.NameToLayer("Enemy") || other.gameObject.layer == LayerMask.NameToLayer("Hittable"))
            {
                EnemyHealth enemyHealth = other.GetComponent<EnemyHealth>();
                if (enemyHealth != null)
                {
                    enemyHealth.TakeDamage(damageAmount);
                }
            }
        }
    }
}
