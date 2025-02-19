using UnityEngine;

public class Gun : MonoBehaviour
{
    [Header("Gun Settings")]
    public float fireRate = 10f;   // Bullets per second
    public float damage = 20f;     // Damage per shot
    public float range = 50f;      // How far the gun can shoot
    public float headshotMultiplier = 2f; // Bonus damage for headshots

    [Header("Declarations")]
    public float nextTimeToFire;

    [Header("Effects")]
    public Camera fpsCamera;       // Assign Main Camera here
    public ParticleSystem muzzleFlash;
    public GameObject impactEffect;
    public AudioSource gunSound;



    private void Update()
    {
        if (Input.GetButton("Fire1") && Time.time >= nextTimeToFire)
        {
            nextTimeToFire = Time.time + 1f / fireRate;
            Shoot();
        }
    }

    private void Shoot()
    {
        // Play effects
        if (muzzleFlash != null) muzzleFlash.Play();
        if (gunSound != null) gunSound.Play();

        // Raycast to check if we hit something
        RaycastHit hit;
        if (Physics.Raycast(fpsCamera.transform.position, fpsCamera.transform.forward, out hit, range))
        {
            Debug.Log("Hit: " + hit.transform.name);

            // Check for enemy health component
            EnemyHealth enemyHealth = hit.transform.GetComponent<EnemyHealth>();
            if (enemyHealth != null)
            {
                float finalDamage = damage;

                // Check for headshot (if the object has the "Head" tag)
                if (hit.collider.CompareTag("Head"))
                {
                    finalDamage *= headshotMultiplier; // Apply headshot bonus
                }

                enemyHealth.TakeDamage(finalDamage);
            }

            // Create bullet impact effect
            if (impactEffect != null)
            {
                GameObject impact = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
                Destroy(impact, 1f);
            }
        }

        // Apply recoil effect to camera
    }

}
