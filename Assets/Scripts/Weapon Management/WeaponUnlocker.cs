using UnityEngine;

public class WeaponUnlocker : MonoBehaviour
{
    public bool unlockGun = false;   // Set to true in the Inspector to unlock the gun
    public bool unlockSword = false; // Set to true in the Inspector to unlock the sword

    private WeaponManager weaponManager;

    void Start()
    {
        // Find the WeaponManager in the scene
        weaponManager = FindObjectOfType<WeaponManager>();
        if (weaponManager == null)
        {
            Debug.LogError("WeaponManager not found in the scene.");
        }
    }

    void OnTriggerEnter(Collider other)
    {
        // Check if the player interacts with the unlocker
        if (other.CompareTag("Player") && weaponManager != null)
        {
            if (unlockGun)
            {
                weaponManager.UnlockWeapon(1); // Assuming gun is at index 1
            }
            if (unlockSword)
            {
                weaponManager.UnlockWeapon(2); // Assuming sword is at index 2
            }

            // Optionally, destroy the unlocker object after unlocking
            Destroy(gameObject);
        }
    }
}
