using UnityEngine;

public class SwordController : MonoBehaviour
{
    public Animator swordAnimator; // Reference to the Animator
    public Collider swordCollider; // Reference to the sword's Collider
    public GameObject swordModel;  // Reference to the GameObject that holds all sword meshes
    public float swingDuration = 0.5f;
    private bool isSwinging = false;

    void Start()
    {
        // Ensure the sword is initially hidden
        swordModel.SetActive(false);
        swordCollider.enabled = false;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !isSwinging)
        {
            int swingType = Random.Range(0, 3);
            string triggerName = swingType switch
            {
                0 => "Swing_Horizontal",
                1 => "Swing_Vertical",
                2 => "Swing_Slant",
                _ => "Swing_Horizontal"
            };

            StartCoroutine(SwingSword(triggerName));
        }
    }

    private System.Collections.IEnumerator SwingSword(string triggerName)
    {
        isSwinging = true;
        swordModel.SetActive(true);
        swordCollider.enabled = true;

        swordAnimator.SetTrigger(triggerName); // Ensure the Animator has this parameter

        yield return new WaitForSeconds(swingDuration);

        swordModel.SetActive(false);
        swordCollider.enabled = false;

        isSwinging = false;
    }

    void OnTriggerEnter(Collider other)
    {
        if (isSwinging && other.CompareTag("Enemy"))
        {
            EnemyHealth enemyHealth = other.GetComponent<EnemyHealth>();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(10);
            }
        }
    }
}
