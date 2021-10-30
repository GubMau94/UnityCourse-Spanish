using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Vector3 movement;
    private Quaternion rotation = Quaternion.identity;

    private Animator _animator;
    private Rigidbody _rigidbody;

    [SerializeField] private float turnSpeed;

    private bool isWalking;

    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
        _rigidbody = GetComponent<Rigidbody>(); 
    }

    void FixedUpdate()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        
        movement.Set(horizontal, 0, vertical);
        movement.Normalize();

        bool hasHorizontalInput = !Mathf.Approximately(horizontal, 0);
        bool hasVerticalInput = !Mathf.Approximately(vertical, 0);
        bool isWalking = hasHorizontalInput || hasVerticalInput;

        _animator.SetBool("isWalking", isWalking);

        Vector3 desiredForward = Vector3.RotateTowards(transform.forward, movement, turnSpeed * Time.fixedDeltaTime, 0f);

        rotation = Quaternion.LookRotation(desiredForward);
    }

    private void OnAnimatorMove()
    {
        //S = S0 + v*t
        _rigidbody.MovePosition(_rigidbody.position + movement * _animator.deltaPosition.magnitude);
        _rigidbody.MoveRotation(rotation);
    }
}
