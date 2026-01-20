using UnityEngine;

public class WheelSpin : MonoBehaviour
{
    public float speed = 90f;

    void Update()
    {
        transform.Rotate(transform.up, speed * Time.deltaTime, Space.World);
    }
}
