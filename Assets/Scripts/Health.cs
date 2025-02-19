using UnityEngine;

public class Health : MonoBehaviour
{
    public int maxHealth = 100;
    public int criticalHealthThreshold = 20; // Below this, player is critically hurt
    private int currentHealth;

    private bool isCriticallyHurt = false;
    private Vector3 lastCheckpoint; // Stores last checkpoint

    private void Start()
    {
        currentHealth = maxHealth;
        lastCheckpoint = transform.position; // Initial spawn point as checkpoint
    }

    public void TakeDamage(int damageAmount)
    {
        currentHealth -= damageAmount;
        Debug.Log("Player took damage. Current Health: " + currentHealth);

        if (currentHealth <= 0)
        {
            PassOut();
        }
        else if (currentHealth <= criticalHealthThreshold)
        {
            isCriticallyHurt = true;
            Debug.Log("Player is critically hurt!");
            // TODO: Apply visual effects like red screen flash
        }
    }

    private void PassOut()
    {
        Debug.Log("Player passed out!");
        isCriticallyHurt = false;
        currentHealth = maxHealth;
        RespawnAtCheckpoint();
    }

    public void SetCheckpoint(Vector3 newCheckpoint)
    {
        lastCheckpoint = newCheckpoint;
        Debug.Log("Checkpoint updated: " + newCheckpoint);
    }

    private void RespawnAtCheckpoint()
    {
        transform.position = lastCheckpoint;
        Debug.Log("Player respawned at checkpoint.");
    }
}
