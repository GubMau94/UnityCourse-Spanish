using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerControllerX : MonoBehaviour
{
    public bool gameOver;

    public float floatForce;
    private float gravityModifier = 1.5f;
    private int pointsCollected;
    
    private Rigidbody playerRb;
    private BoxCollider _boxCollider;

    public ParticleSystem explosionParticle;
    public ParticleSystem fireworksParticle;

    private AudioSource playerAudio;
    public AudioClip moneySound;
    public AudioClip explodeSound;
    public AudioClip moveUpSound;


    // Start is called before the first frame update
    void Start()
    {
        Physics.gravity = gravityModifier * new Vector3(0, -9.81f, 0);
        playerAudio = GetComponent<AudioSource>();
        playerRb = GetComponent<Rigidbody>();
        _boxCollider = GetComponent<BoxCollider>();
        
        // Apply a small upward force at the start of the game
        playerRb.AddForce(Vector3.up * 5, ForceMode.Impulse);

        
    }

    // Update is called once per frame
    void Update()
    {
        // While space is pressed and player is low enough, float up
        if (Input.GetKeyDown(KeyCode.Space) && !gameOver)
        {
            playerRb.AddForce(Vector3.up * floatForce, ForceMode.Impulse);
            playerAudio.PlayOneShot(moveUpSound, 1.0f);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // if player collides with bomb, explode and set gameOver to true
        if (other.gameObject.CompareTag("Bomb"))
        {
            explosionParticle.Play();
            playerAudio.PlayOneShot(explodeSound, 1.0f);
            gameOver = true;
            Debug.Log("Game Over!");
            Destroy(other.gameObject);

            _boxCollider.material.bounciness = 0f;
            Invoke("RestartGame", 2f);

        }
        // if player collides with money, fireworks
        else if (other.gameObject.CompareTag("Money"))
        {
            fireworksParticle.Play();
            playerAudio.PlayOneShot(moneySound, 1.0f);
            Destroy(other.gameObject);
            pointsCollected++;
            print("You have: " + pointsCollected + " points");
        }
    }

  

    void RestartGame()
    {
        SceneManager.LoadSceneAsync("Challenge 3");
    }

}
