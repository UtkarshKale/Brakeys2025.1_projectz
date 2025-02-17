using UnityEngine;

public class Sway : MonoBehaviour
{
    public float swayAmount = 0.02f;       // Amount of sway
    public float maxSwayAmount = 0.06f;    // Maximum sway
    public float smoothTime = 0.1f;        // Smooth time for SmoothDamp

    private Vector3 initialPosition;
    private Vector3 currentVelocity;

    void Start()
    {
        initialPosition = transform.localPosition;
    }

    void Update()
    {
        // Get mouse input
        float moveX = -Input.GetAxis("Mouse X") * swayAmount;
        float moveY = -Input.GetAxis("Mouse Y") * swayAmount;

        // Clamp sway values
        moveX = Mathf.Clamp(moveX, -maxSwayAmount, maxSwayAmount);
        moveY = Mathf.Clamp(moveY, -maxSwayAmount, maxSwayAmount);

        // Calculate target position
        Vector3 targetPosition = new Vector3(moveX, moveY, 0f) + initialPosition;

        // Smoothly move to the target position
        transform.localPosition = Vector3.SmoothDamp(transform.localPosition, targetPosition, ref currentVelocity, smoothTime);
    }
}
