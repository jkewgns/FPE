using UnityEngine;
using Unity.Cinemachine;

public class CameraFollow : MonoBehaviour
{
    public Transform carTarget;
    public Rigidbody carRb;
    public Vector3 headOffset = new Vector3(0, 2, -5);

    [Range(0, 1)] public float steeringInfluence = 0.8f;
    [Range(0, 1)] public float velocityInfluence = 0.6f;
    public float lookSmoothness = 10f;

    private Transform virtualCamTransform;

    void Start()
    {
        virtualCamTransform = GetComponent<CinemachineCamera>()?.transform;
    }

    void FixedUpdate()
    {
        if (!carTarget || !carRb || virtualCamTransform == null) return;

        Vector3 targetPos = carTarget.TransformPoint(headOffset);
        virtualCamTransform.position = Vector3.Lerp(virtualCamTransform.position, targetPos, Time.fixedDeltaTime * 5f);

        Vector3 carForward = carTarget.forward;
        Vector3 velocityDir = carRb.linearVelocity.magnitude > 1f ? carRb.linearVelocity.normalized : carForward;

        Vector3 lookDir = Vector3.Slerp(carForward, velocityDir, velocityInfluence);
        float steerInput = Input.GetAxis("Horizontal");
        Quaternion steerLook = Quaternion.AngleAxis(steerInput * 30f * steeringInfluence, carTarget.up);
        Quaternion targetRotation = Quaternion.LookRotation(lookDir, carTarget.up) * steerLook;

        virtualCamTransform.rotation = Quaternion.Slerp(virtualCamTransform.rotation, targetRotation, Time.fixedDeltaTime * lookSmoothness);
    }
}
