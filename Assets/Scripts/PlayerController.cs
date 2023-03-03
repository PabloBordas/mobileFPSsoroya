using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public float cameraSensibility;
    public FixedJoystick fixedJoystick;
    public FixedJoystick fixedJoystickCamera;
    public Rigidbody rb;
    public Camera playerCamera;
    float cameraVerticalAngle;
    float cameraHorizontalAngle;

    public void FixedUpdate()
    {
        Vector3 direction = Vector3.forward * fixedJoystick.Vertical + Vector3.right * fixedJoystick.Horizontal;
        transform.rotation = GyroToUnity(Input.gyro.attitude);
        rb.AddForce(direction * speed * Time.fixedDeltaTime, ForceMode.VelocityChange);

        MoveCamera();

    }

    private static Quaternion GyroToUnity(Quaternion q)
    {
        return new Quaternion(q.x, q.y, -q.z, -q.w);
    }

    public void MoveCamera(){
        cameraVerticalAngle += fixedJoystickCamera.Vertical;
        cameraVerticalAngle = Mathf.Clamp(cameraVerticalAngle, -60f, 60f);

        cameraHorizontalAngle += fixedJoystickCamera.Horizontal;;

        //transform.Rotate(Vector3.up * mouseInput.x);
        playerCamera.transform.localRotation = Quaternion.Euler(-cameraVerticalAngle, cameraHorizontalAngle, 0f);
    }
}
