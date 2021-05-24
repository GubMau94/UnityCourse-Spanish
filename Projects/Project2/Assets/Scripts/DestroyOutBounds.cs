using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOutBounds : MonoBehaviour
{
    [SerializeField] private float zBoundUpper = 20f;
    [SerializeField] private float zBoundLower = -15f;
    
    // Update is called once per frame
    void Update()
    {
        if (transform.position.z < zBoundLower || transform.position.z > zBoundUpper)
        {
            Destroy(gameObject);
        }
    }
}
