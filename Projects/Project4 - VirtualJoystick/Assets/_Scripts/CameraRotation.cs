using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotation : MonoBehaviour
{
    [SerializeField] private float rotationSpeed;

    private float rotationInput;

    // Update is called once per frame
    void Update()
    {
        rotationInput = Input.GetAxis("Horizontal");
        transform.Rotate(Vector3.down * rotationSpeed * Time.deltaTime * rotationInput);
    }
}
