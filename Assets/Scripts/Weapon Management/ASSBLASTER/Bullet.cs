using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 20f;
    public float lifetime = 2f;
    public int damage = 10;
    public string shooterTag; // To prevent self-damage

    private void Start()
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.linearVelocity = transform.forward * speed; // Fixed velocity assignment
        Destroy(gameObject, lifetime);
    }

    private void OnTriggerEnter(Collider other)
    {
        // Prevent the bullet from damaging the shooter
        if (other.CompareTag(shooterTag))
            return;

        // Check if the hit object has a Health component
        Health health = other.GetComponent<Health>();
        if (health != null)
        {
            health.TakeDamage(damage);
        }

        // Destroy bullet on impact
        Destroy(gameObject);
    }
}
