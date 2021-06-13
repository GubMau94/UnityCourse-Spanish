using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    private float verticalForce, torqueForce, spawnPos;
    
    private Rigidbody _rigidbody;

    private GameObject spawner;



    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        spawner = GameObject.Find("SpawnManager");

        verticalForce = Random.Range(10, 15);
        torqueForce = Random.Range(-20, 20);
        spawnPos = Random.Range(-4.1f, 4.1f);

        _rigidbody.AddForce(Vector3.up * verticalForce, ForceMode.Impulse);
        _rigidbody.AddTorque(torqueForce, torqueForce, torqueForce);
        transform.position = new Vector3(spawnPos, spawner.transform.position.y);
    }

    private void OnMouseOver()
    {
        if (Input.GetMouseButton(0))
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("KillZone"))
        {
            Destroy(gameObject);
        }
    }


} //class

































