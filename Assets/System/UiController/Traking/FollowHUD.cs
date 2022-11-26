using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Takechi.UI.Mune.Tracking
{
    public class FollowHUD : MonoBehaviour
    {
        public Transform Target;
        [SerializeField] private float followMoveSpeed = 0.1f;
        [SerializeField] private float followRotateSpeed = 0.02f;
        [SerializeField] private float rotateSpeedThreshold = 0.9f;
        [SerializeField] private bool isImmediateMove;
        [SerializeField] private bool isLockX;
        [SerializeField] private bool isLockY;
        [SerializeField] private bool isLockZ;

        private Quaternion rot;
        private Quaternion rotDif;

        private void Start ()
        {
            if (!Target) Target = UnityEngine.Camera.main.transform.GetChild(0);
        }

        private void LateUpdate ()
        {
            if(isImmediateMove) transform.position = Target.position;
            else transform.position = Vector3.Lerp( transform.position, Target.position, followMoveSpeed);

            rotDif = Target.rotation * Quaternion.Inverse(transform.rotation);

            rot = Target.rotation;
            if(isLockX) rot.x = 0;
            if(isLockY) rot.y = 0;
            if(isLockZ) rot.z = 0;
            if(rotDif.w < rotateSpeedThreshold) transform.rotation = Quaternion.Lerp(transform.rotation, rot, followRotateSpeed * 4);
            else transform.rotation = Quaternion.Lerp(transform.rotation, rot, followRotateSpeed);
        }

        public void ImmediateSync (Transform targetTransform)
        {
            transform.position = targetTransform.position;
            transform.rotation = targetTransform.rotation;
        }
    }
}