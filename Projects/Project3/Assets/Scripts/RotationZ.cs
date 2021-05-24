using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationZ : MonoBehaviour
{
    private float speedRotation = 180f;
    
    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.down * (Time.deltaTime * speedRotation));    
    }
}
