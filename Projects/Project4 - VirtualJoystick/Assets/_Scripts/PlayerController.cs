using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float movementForce, boundFocePowerup;
    [SerializeField] private GameObject origin;
    private Rigidbody _rigidbody;
    private Rigidbody _enemyRigidbody;

    private bool powerActive;
    private float powerTime = 7f;
    [SerializeField] private GameObject[] powerUpIndicators;

    private float verticalInput;
    private float horizontalInput;

    private Joystick _joystick;

    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();

        _joystick = FindObjectOfType<Joystick>();
    }

    // Update is called once per frame
    void Update()
    {
        //verticalInput = Input.GetAxis("Vertical");
        verticalInput = _joystick.Vertical;
        horizontalInput = _joystick.Horizontal;

        _rigidbody.AddForce(origin.transform.forward * movementForce * verticalInput, ForceMode.Force);

        _rigidbody.AddForce(origin.transform.right * movementForce * horizontalInput, ForceMode.Force);

        foreach (GameObject indicator in powerUpIndicators)
        {
            indicator.transform.position = transform.position + 0.5f * Vector3.down;
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PowerUp"))
        {
            powerActive = true;
            Destroy(other.gameObject);

            StartCoroutine(PowerUpIndicator());

        }

        if (other.CompareTag("MapOutside"))
        {
            SceneManager.LoadSceneAsync("Prototype 4");
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Enemy") && powerActive)
        {
            Vector3 boundDirection = collision.gameObject.transform.position - transform.position;
            _enemyRigidbody = collision.gameObject.GetComponent<Rigidbody>();
            _enemyRigidbody.AddForce(boundDirection * boundFocePowerup, ForceMode.Impulse);
           
        }
    }

    IEnumerator PowerUpIndicator()
    {
        foreach (GameObject indicator in powerUpIndicators)
        {
            indicator.SetActive(true);
            yield return new WaitForSeconds(powerTime / powerUpIndicators.Length);
            indicator.SetActive(false);
        }

        powerActive = false;
    }

}



