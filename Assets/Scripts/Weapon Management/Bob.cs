using UnityEngine;

public class Bob : MonoBehaviour
{
    [Header("Bobbing Settings")]
    public float walkBobbingSpeed = 14f;
    public float sprintBobbingSpeed = 18f;
    public float bobbingAmount = 0.05f;
    public float transitionSpeed = 5f; // Speed at which bobbing intensity transitions

    private float defaultPosY;
    private float timer = 0f;
    private float bobbingIntensity = 0f; // Current intensity of the bobbing effect

    public float smoothTime = 0.1f;


    private PlayerMovement playerMovement;


    void Start()
    {
        // Store the initial local position Y of the sword
        defaultPosY = transform.localPosition.y;

        // Reference to the PlayerMovement script
        playerMovement = Object.FindFirstObjectByType<PlayerMovement>();
        if (playerMovement == null)
        {
            Debug.LogError("PlayerMovement script not found in the scene.");
        }
    }

private Vector3 currentVelocity;

void LateUpdate()
{
    if (playerMovement == null)
        return;

    // Determine if the player is moving
    bool isMoving = playerMovement.state == PlayerMovement.MovementState.walking ||
                    playerMovement.state == PlayerMovement.MovementState.sprinting;

    if (isMoving && playerMovement.IsGrounded)
    {
        // Calculate bobbing effect
        float bobbingSpeed = playerMovement.state == PlayerMovement.MovementState.sprinting ? sprintBobbingSpeed : walkBobbingSpeed;
        timer += Time.deltaTime * bobbingSpeed;
        float bobbingOffset = Mathf.Sin(timer) * bobbingAmount;

        // Determine target position
        Vector3 targetPosition = new Vector3(transform.localPosition.x, defaultPosY + bobbingOffset, transform.localPosition.z);

        // Smoothly move to the target position
        transform.localPosition = Vector3.SmoothDamp(transform.localPosition, targetPosition, ref currentVelocity, smoothTime);
    }
    else
    {
        // Reset timer and smoothly return to the default position
        timer = 0f;
        Vector3 targetPosition = new Vector3(transform.localPosition.x, defaultPosY, transform.localPosition.z);
        transform.localPosition = Vector3.SmoothDamp(transform.localPosition, targetPosition, ref currentVelocity, smoothTime);
    }
}

}
