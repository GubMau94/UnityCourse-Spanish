using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed = 10f;
    private float xMovement;
    private float zMovement;
    [SerializeField] private float xBounds = 14f;
    [SerializeField] private float zBounds = 7f;
    [SerializeField] private GameObject projectilePrefab;
   
    // Update is called once per frame
    void Update()
    {
        Movements();
        
        xLimits();
        zLimits();
        
        ProjectileSpawner();
    }

    void Movements()
    {
        xMovement = Input.GetAxis("Horizontal");
        transform.Translate(Vector3.right * (speed * Time.deltaTime * xMovement));
        
        zMovement = Input.GetAxis("Vertical");
        transform.Translate(Vector3.forward * (speed * Time.deltaTime * zMovement));
    }
    
    
    void xLimits()
    {
        // Limiti movimento in X
        if (transform.position.x > xBounds)
        {
            transform.position = new Vector3(xBounds, transform.position.y, transform.position.z);
        }
        if (transform.position.x < -xBounds)
        {
            transform.position = new Vector3(-xBounds, transform.position.y, transform.position.z);
        }
    }

    void zLimits()
    {
        // Limiti movimento in Z
        if (transform.position.z > zBounds)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, zBounds);
        }
        if (transform.position.z < -zBounds)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, -zBounds);
        }
    }

    void ProjectileSpawner()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Instantiate(projectilePrefab, transform.position, transform.rotation);
        }
        
    }
    
    
    
} //class
