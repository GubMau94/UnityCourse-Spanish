using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManagement : MonoBehaviour
{
    [SerializeField] private float forceMevement;
    private Rigidbody _rigidbody;
    private GameObject player;


    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 direction = (player.transform.position - transform.position).normalized;
        _rigidbody.AddForce(direction * forceMevement * Time.fixedDeltaTime, ForceMode.Force);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("MapOutside"))
        {
            Destroy(gameObject);
        }
    }

}
