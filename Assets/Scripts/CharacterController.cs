using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LongMethod
{
    public class CharacterController : MonoBehaviour
    {
        [SerializeField] private float jumpSpeed = 10;
        [SerializeField] private float positionModifier = 7;
        [SerializeField] private float rotationModifier = 45;
        [SerializeField] private float minCameraRotation = 10;
        [SerializeField] private float maxCameraRotation = 35;

        private void Start()
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
        void Update()
        {
            MoveCharacter();
            RotateCamera();
        }

        private void RotateCamera()
        {
            var mainCamera = transform.Find("Main Camera").transform;
            var xRotation = mainCamera.localRotation.eulerAngles.x - rotationModifier * Input.GetAxis("Mouse Y") * Time.deltaTime;
            if (xRotation > maxCameraRotation)
            {
                xRotation = maxCameraRotation;
            }
            else if (xRotation < minCameraRotation)
            {
                xRotation = minCameraRotation;
            }
            mainCamera.localRotation = Quaternion.Euler(xRotation, 0, 0);
        }

        private void MoveCharacter()
        {
            var movement = (Input.GetAxis("Horizontal") * transform.right + Input.GetAxis("Vertical") * transform.forward).normalized;
            transform.position += positionModifier * Time.deltaTime * movement;
            transform.localRotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y + rotationModifier *
                                                                                 Input.GetAxis("Mouse X") * Time.deltaTime, 0);

            if (Input.GetButtonDown("Jump"))
                Jump();
        }

        private void Jump()
        {
            GetComponent<Rigidbody>().velocity = Vector3.up * jumpSpeed;
        }

    }
}
