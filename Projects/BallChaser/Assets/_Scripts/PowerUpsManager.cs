using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpsManager : MonoBehaviour
{
    private Rigidbody _rigidbody;
    private float rotationForce = 0.1f;

    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        //Applica un effetto di rotazione
        _rigidbody.AddTorque(Vector3.up * rotationForce, ForceMode.Impulse);
    }
}
