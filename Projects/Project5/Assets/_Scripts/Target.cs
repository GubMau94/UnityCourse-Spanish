using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Target : MonoBehaviour
{ 
    private Rigidbody _rigidbody;
    [SerializeField] private GameObject particles;

    private GameObject spawner;

    private GameManager _gameManager;
    [SerializeField, Range(-100,100)] private int points;



    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        spawner = GameObject.Find("SpawnManager");
        _gameManager = FindObjectOfType<GameManager>();

        _rigidbody.AddForce(RandomVerticalForce(), ForceMode.Impulse);
        _rigidbody.AddTorque(RandomTorqueForce(), ForceMode.Impulse);
        transform.position = RandomPos();
    }

    /// <summary>
    /// Applico una forza verticale aleatoria
    /// </summary>
    /// <returns>Vector3</returns>
    private Vector3 RandomVerticalForce()
    {
        float verticalForce = Random.Range(12, 15);
        return (Vector3.up * verticalForce);
    }

    /// <summary>
    /// Applico una forza di rotazione in X, Y e Z
    /// </summary>
    /// <returns>Vector3</returns>
    private Vector3 RandomTorqueForce()
    {
        float torqueForce = Random.Range(-20, 20);
        float torqueForceX = Random.Range(10, 20);
        Vector3 torque = new Vector3(torqueForceX, torqueForce, torqueForce);
        return torque;
    }

    /// <summary>
    /// Genero un Vector3 con X aleatoria tra -4.1 e +4.1
    /// </summary>
    /// <returns>Vector3 con X aleatoria, Y e Z uguali al gameObject</returns>
    private Vector3 RandomPos()
    {
        float spawnPos = Random.Range(-4.1f, 4.1f);
        Vector3 pos = new Vector3(spawnPos, spawner.transform.position.y);
        return pos;
    }

    private void OnMouseOver()
    {
        if (Input.GetMouseButton(0) && _gameManager._gameState == GameManager.GameState.inGame)
        {
            _gameManager.UpdateScore(points);
            Instantiate(particles, transform.position, particles.transform.rotation);
            Destroy(gameObject);
            
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("KillZone"))
        {
            Destroy(gameObject);
            if (CompareTag("Good"))
            {
                _gameManager.GameOver();
            }
        }
    }


} //class

































