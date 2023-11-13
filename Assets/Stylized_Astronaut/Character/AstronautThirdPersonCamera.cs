using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AstronautThirdPersonCamera
{
    public class AstronautThirdPersonCamera : MonoBehaviour
    {
        private const float Y_ANGLE_MIN = 0.0f;
        private const float Y_ANGLE_MAX = 50.0f;

        public Transform lookAt;
        public Transform camTransform;
        public float distance = 5.0f;
        public float followSpeed = 5.0f;

        private float currentX = 0.0f;
        private float currentY = 45.0f;
        private float sensitivityX = 5.0f;
        private float sensitivityY = 5.0f;
        private float smoothTime = 0.1f;
        private Vector3 velocity;

        private Vector3 lastLookAtPosition;

        private void Start()
        {
            camTransform = transform;
            lastLookAtPosition = lookAt.position;
        }

        private void Update()
        {
            currentX += Input.GetAxis("Mouse X") * sensitivityX;
            currentY -= Input.GetAxis("Mouse Y") * sensitivityY;

            currentY = Mathf.Clamp(currentY, Y_ANGLE_MIN, Y_ANGLE_MAX);
        }

        private void LateUpdate()
        {
            Vector3 dir = new Vector3(0, 0, -distance);
            Quaternion rotation = Quaternion.Euler(currentY, currentX, 0);
            Vector3 targetPosition = lookAt.position + rotation * dir;

            // Tính toán hướng di chuyển của nhân vật
            Vector3 moveDirection = lookAt.position - lastLookAtPosition;

            // Áp dụng hướng di chuyển vào vị trí của camera
            targetPosition += moveDirection * followSpeed;

            lastLookAtPosition = lookAt.position;

            camTransform.position = Vector3.SmoothDamp(camTransform.position, targetPosition, ref velocity, smoothTime);
            camTransform.LookAt(lookAt.position);
        }
    }
}