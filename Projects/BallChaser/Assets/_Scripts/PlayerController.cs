using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float movementForce, boundFocePowerup, jumpForce;
    [SerializeField] private GameObject origin;
    private Rigidbody _rigidbody;
    private Rigidbody _enemyRigidbody;

    private bool forcePowerActive;
    private bool jumpPowerActive;
    private bool wallPowerActive;

    private float powerTime = 7f;

    [SerializeField] private GameObject wallPower;

    [SerializeField] private GameObject[] powerUpIndicators;

    private float verticalInput;
    private float horizontalInput;

    public static int gold;
    public static string GOLD = "GOLD";
    private Text _goldText;

    private bool onGround = true;

    private Joystick _joystick;
    private Joybutton _joybutton;
    [SerializeField] private GameObject joystickPos;

    private SpawnManager _spawnManager;

    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();

        _joystick = FindObjectOfType<Joystick>();
        _joybutton = FindObjectOfType<Joybutton>();

        _spawnManager = FindObjectOfType<SpawnManager>();

        _goldText = GameObject.Find("Gold").GetComponent<Text>();

        gold = PlayerPrefs.GetInt("GOLD"); ;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(_spawnManager.gameState == SpawnManager.GameState.inGame)
        {
            verticalInput = _joystick.Vertical + Input.GetAxis("Vertical");
            horizontalInput = _joystick.Horizontal + Input.GetAxis("Horizontal");

            _rigidbody.AddForce(origin.transform.forward * movementForce * verticalInput * Time.fixedDeltaTime, ForceMode.Force);
            _rigidbody.AddForce(origin.transform.right * movementForce * horizontalInput * Time.fixedDeltaTime, ForceMode.Force);

            foreach (GameObject indicator in powerUpIndicators)
            {
                indicator.transform.position = transform.position + 0.5f * Vector3.down;
            }
        }
        
    }

    private void Update()
    {
        if ((Input.GetKeyDown(KeyCode.Space) || _joybutton.pressed) && onGround && jumpPowerActive)
        {
            _rigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            onGround = false;
        }

        _goldText.text = "Gold: " + gold;

        JoystickPosition();
    }

    private void JoystickPosition()
    {
        if (MainMenuManager.joystickLeft)
        {
            joystickPos.transform.position = new Vector3(350,350,0);
        } 
        else
        {
            joystickPos.transform.position = new Vector3(1800, 350);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //Permette al player di avere la super forza
        if (other.CompareTag("PowerUp") && !forcePowerActive && !jumpPowerActive)
        {
            forcePowerActive = true;
            powerTime = 7f;
            Destroy(other.gameObject);

            StartCoroutine(PowerUpIndicator());
        }

        //Permette al player di saltare per 3 secondi
        if (other.CompareTag("jumpPower") && !forcePowerActive && !jumpPowerActive)
        {
            jumpPowerActive = true;
            powerTime = 3f;
            Destroy(other.gameObject);

            StartCoroutine(PowerUpIndicator());
        }

        //Evita che il player cada dall'arena
        if (other.CompareTag("wallPower") && !wallPowerActive)
        {
            wallPowerActive = true;
            powerTime = 3f;
            wallPower.SetActive(true);
            Destroy(other.gameObject);

            StartCoroutine(PowerUpIndicator());
        }

        if (other.CompareTag("goldPower"))
        {
            gold += 5;
            PlayerPrefs.SetInt(GOLD, gold);

            Destroy(other.gameObject);
        }

        //Se il player esce dalla mappa rinizia il gioco
        if (other.CompareTag("MapOutside"))
        {
            SceneManager.LoadSceneAsync("Gameplay");
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Enemy") && forcePowerActive)
        {
            Vector3 boundDirection = collision.gameObject.transform.position - transform.position;
            _enemyRigidbody = collision.gameObject.GetComponent<Rigidbody>();
            _enemyRigidbody.AddForce(boundDirection * boundFocePowerup, ForceMode.Impulse);
           
        }

        if (collision.gameObject.CompareTag("Ground"))
        {
            onGround = true;
        }
    }

    IEnumerator PowerUpIndicator()
    {
        if (!wallPowerActive)
        {
            foreach (GameObject indicator in powerUpIndicators)
            {
                indicator.SetActive(true);
                yield return new WaitForSeconds(powerTime / powerUpIndicators.Length);
                indicator.SetActive(false);
            }

            forcePowerActive = false;
            jumpPowerActive = false;
        }
        else
        {
            yield return new WaitForSeconds(powerTime);
            wallPower.SetActive(false);
            wallPowerActive = false;
        }
        
    }

}



