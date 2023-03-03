using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public Vector3 destination;
    public float bulletSpeed = 3f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, destination, Time.deltaTime * bulletSpeed);
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.LogWarning("Projectile OnTriggerEnter");

        if (other.tag == "Enemy")
        {
            Destroy(other.gameObject);
            Destroy(gameObject);
            GameController.gameControllerInstance.currentEnemies -= 1;
        }else if(other.tag == "Enviroment"){
            Destroy(gameObject);
        }
    }

}