using UnityEngine;

public class Sway : MonoBehaviour
{
    public float swayAmount = 0.02f;       // Amount of sway
    public float maxSwayAmount = 0.06f;    // Maximum sway
    public float smoothAmount = 6f;        // Smoothness of the sway

    private Vector3 initialPosition;

    void Start()
    {
        initialPosition = transform.localPosition;
    }

    void Update()
    {
        float moveX = -Input.GetAxis("Mouse X") * swayAmount;
        float moveY = -Input.GetAxis("Mouse Y") * swayAmount;

        moveX = Mathf.Clamp(moveX, -maxSwayAmount, maxSwayAmount);
        moveY = Mathf.Clamp(moveY, -maxSwayAmount, maxSwayAmount);

        Vector3 finalPosition = new Vector3(moveX, moveY, 0f) + initialPosition;
        transform.localPosition = Vector3.Lerp(transform.localPosition, finalPosition, Time.deltaTime * smoothAmount);
    }
}
