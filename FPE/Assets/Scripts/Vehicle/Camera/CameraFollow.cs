using UnityEngine;
using Unity.Cinemachine;

public class CameraFollow : MonoBehaviour
{
   [Header("Attachments")]
   public Transform carTarget;
   public Rigidbody carRigidbody;

   [Header("Positioning")]
   public Vector3 headOffset = new Vector3(0, 0, 0);

   [Header("Neck Movement")]
   [Range(0, 1)] public float steeringInfluence = 0.5f;
   [Range(0, 1)] public float velocityInfluence = 0.3f;
   public float lookSmoothness = 10f;
   private Transform virtualCamTransform;

   void Start()
   {
       virtualCamTransform = GetComponent<CinemachineCamera>()?.transform;
   }

   void FixedUpdate()
   {
       if (!carTarget || !carRigidbody || virtualCamTransform == null) return;

       Vector3 targetPos = carTarget.TransformPoint(headOffset);
       virtualCamTransform.position = targetPos;

       Vector3 carForward = carTarget.forward;
       Vector3 velocityDir = carRigidbody.linearVelocity.normalized;
       if (carRigidbody.linearVelocity.magnitude < 1f)
           velocityDir = carForward;
       Vector3 lookDir = Vector3.Slerp(carForward, velocityDir, velocityInfluence);
       float steerInput = Input.GetAxis("Horizontal");
       Quaternion steerLook = Quaternion.AngleAxis(steerInput * 30f * steeringInfluence, carTarget.up);
       Quaternion targetRotation = Quaternion.LookRotation(lookDir, carTarget.up) * steerLook;

       virtualCamTransform.rotation = Quaternion.Slerp(virtualCamTransform.rotation, targetRotation, Time.fixedDeltaTime * lookSmoothness);
   }
}