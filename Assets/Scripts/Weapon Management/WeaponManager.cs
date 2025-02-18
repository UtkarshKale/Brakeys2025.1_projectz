using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    public GameObject[] weapons; // Array to hold weapon GameObjects
    private bool[] weaponUnlocked; // Tracks which weapons are unlocked
    private int currentWeaponIndex = 0; // Index of the currently active weapon

    void Start()
    {
        // Initialize weaponUnlocked array based on the number of weapons
        weaponUnlocked = new bool[weapons.Length];

        // Unlock the default weapon (hands)
        weaponUnlocked[0] = true;

        // Deactivate all weapons except the default one
        for (int i = 0; i < weapons.Length; i++)
        {
            weapons[i].SetActive(i == currentWeaponIndex);
        }
    }

    void Update()
    {


          // Check for weapon switch input
    for (int i = 0; i < weapons.Length; i++)
    {
        if (Input.GetKeyDown(KeyCode.Alpha1 + i))
        {
            Debug.Log($"Key {i + 1} pressed.");
            if (weaponUnlocked[i])
            {
                SwitchWeapon(i);
            }
            else
            {
                Debug.Log($"Weapon {i} is locked.");
            }
        }
    }

        // Check for weapon switch input
        for (int i = 0; i < weapons.Length; i++)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1 + i) && weaponUnlocked[i])
            {
                SwitchWeapon(i);
            }
        }
    }

    // Switches to the weapon at the given index
    void SwitchWeapon(int index)
    {
        if (index >= 0 && index < weapons.Length && weaponUnlocked[index])
        {
            weapons[currentWeaponIndex].SetActive(false);
            weapons[index].SetActive(true);
            currentWeaponIndex = index;
        }
    }

    // Unlocks the weapon at the given index
    public void UnlockWeapon(int index)
    {
        if (index >= 0 && index < weapons.Length)
        {
            weaponUnlocked[index] = true;
        }
    }
}
