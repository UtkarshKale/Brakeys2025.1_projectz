using System.Collections;
using UnityEngine;

public class SwordVFXController : MonoBehaviour
{
    public GameObject vfxPrefab; // Assign the slash VFX prefab in the Inspector
    public Transform vfxSpawnPoint; // Assign the empty GameObject (spawn position)
    public float delayBeforeVFX = 0.2f; // Adjust this to sync with the animation

    private GameObject currentVFX;

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Left-click to attack
        {
            StartCoroutine(PlaySwordVFX());
        }
    }

    IEnumerator PlaySwordVFX()
    {
        // Wait before playing the VFX (sync with attack animation)
        yield return new WaitForSeconds(delayBeforeVFX);

        // Instantiate the VFX at the spawn point
        if (vfxPrefab != null && vfxSpawnPoint != null)
        {
            currentVFX = Instantiate(vfxPrefab, vfxSpawnPoint.position, vfxSpawnPoint.rotation);
        }
    }
}
