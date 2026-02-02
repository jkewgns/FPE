using UnityEngine;

public class CarAudio : MonoBehaviour
{
    public AudioSource Engine;
    public AudioSource GearChange;
    public Rigidbody carRb;

    public float Pitch1 = 0.5f;
    public float Pitch2 = 0.7f;
    public float Pitch3 = 0.9f;
    public float Pitch4 = 1.1f;
    public float Pitch5 = 1.3f;
    public float Pitch6 = 1.5f;

    void Update()
    {
        UpdatePitch();
        GearChangeCheck();
    }

    void UpdatePitch()
    {
        float speed = carRb.linearVelocity.magnitude * 3.6f;

        if (speed < 30) Engine.pitch = speed * Pitch1;
        else if (speed < 60) Engine.pitch = speed * Pitch2;
        else if (speed < 90) Engine.pitch = speed * Pitch3;
        else if (speed < 120) Engine.pitch = speed * Pitch4;
        else if (speed < 150) Engine.pitch = speed * Pitch5;
        else Engine.pitch = speed * Pitch6;
    }

    void GearChangeCheck()
    {
        float speed = carRb.linearVelocity.magnitude * 3.6f;

        if ((speed > 30 && speed < 31) || (speed > 60 && speed < 61) ||
            (speed > 90 && speed < 91) || (speed > 120 && speed < 121) ||
            (speed > 150 && speed < 151) || (speed > 180 && speed < 181))
        {
            if (!GearChange.isPlaying) GearChange.Play();
        }
    }
}
