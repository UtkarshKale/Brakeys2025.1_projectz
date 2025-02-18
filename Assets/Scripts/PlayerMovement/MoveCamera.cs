using UnityEngine;

public class MoveCamera : MonoBehaviour
{
    public Transform camoPos;
    private void Update()
    {
        transform.position = camoPos.position;
    }
}
