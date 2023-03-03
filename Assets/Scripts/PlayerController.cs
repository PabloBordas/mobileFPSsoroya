using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public float cameraSensibility;
    public FixedJoystick fixedJoystick;
    public FixedJoystick fixedJoystickCamera;
    public GameObject firePoint;
    public GameObject projectilePrefab;
    public Rigidbody rb;
    public Camera playerCamera;
    public float recoilTime;    
    public bool isRecoiling;

    float cameraVerticalAngle;
    Vector3 moveInput;
    Vector3 mouseInput;
    float previousAcceleration;
    float currentAcceleration;

    void Start()
    {
        StartCoroutine(CheckForShoots());
        isRecoiling = false;
    }

    void Update(){
        
    }

    public void FixedUpdate()
    {
        MovePlayer();
        MoveCamera();
    }

    public void MovePlayer()
    {
        moveInput = new Vector3(fixedJoystick.Horizontal, 0f, fixedJoystick.Vertical);
        moveInput = Vector3.ClampMagnitude(moveInput, 1f);

        moveInput = transform.TransformDirection(moveInput) * speed;
        rb.velocity = moveInput * Time.deltaTime;

        //characterController.Move(moveInput * Time.deltaTime);
    }

    public void MoveCamera()
    {
        mouseInput.x = fixedJoystickCamera.Horizontal * cameraSensibility * Time.deltaTime;
        mouseInput.y = fixedJoystickCamera.Vertical * cameraSensibility * Time.deltaTime;

        cameraVerticalAngle += mouseInput.y;
        cameraVerticalAngle = Mathf.Clamp(cameraVerticalAngle, -60f, 60f);

        transform.Rotate(Vector3.up * mouseInput.x);
        playerCamera.transform.localRotation = Quaternion.Euler(-cameraVerticalAngle, 0f, 0f);
    }

    IEnumerator CheckForShoots()
    {
        currentAcceleration = Input.acceleration.y;
        if(Mathf.Abs(previousAcceleration) - Mathf.Abs(currentAcceleration) > 0.2f ){
            Debug.Log("Disparo");
            if(!isRecoiling){
                StartCoroutine(Shoot());
            }   
        }
        previousAcceleration = currentAcceleration;
        yield return new WaitForSeconds(0.3f);
        StartCoroutine(CheckForShoots());
        
    }

    IEnumerator Shoot()
    {
        isRecoiling = true;
        Ray ray = playerCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 500f))
        {
            projectilePrefab.GetComponent<Projectile>().destination = hit.point;
            GameObject basicAttack = Instantiate(projectilePrefab, firePoint.transform.position, transform.rotation);
        }
        else
        {
            Debug.Log("I´m looking at nothing");
        }
        yield return new WaitForSeconds(recoilTime);
        isRecoiling = false;
    }
}
