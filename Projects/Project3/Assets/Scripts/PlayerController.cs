using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;


public class PlayerController : MonoBehaviour
{
    private const string SpeedF = "Speed_f";
    private const string StaticB = "Static_b";
    private const string JumpTrig = "Jump_trig";
    private const string DeathB = "Death_b";
    private const string DeathtypeINT = "DeathType_int";


    private Rigidbody _rigidbody;
    private Animator _animator;
    private BoxCollider _boxCollider;
    
    [SerializeField] private ParticleSystem explosion;
    [SerializeField] private ParticleSystem dirt;

    [SerializeField] private GameObject groundCheck;
    [SerializeField] private LayerMask _layerMask;
    
    private AudioSource _audioSource;
    [SerializeField] private AudioClip crashClip, jumpClip;
    
    private float jumpForce = 10f;
    [SerializeField, Range(0, 1)] private float volumeClip = 1;
    private float _gravity = 2f;

    private float actualCenterY, actualSizeY;
    
    
    private bool onGround = true;
    private bool _gameOver;
    public bool groundChecked;
    
    public bool GameOver { get => _gameOver; }

    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _animator = GetComponent<Animator>();
        
        _animator.SetFloat(SpeedF, 1);
        _animator.SetBool(StaticB, true);

        _audioSource = GetComponent<AudioSource>();
        _boxCollider = GetComponent<BoxCollider>();

        actualSizeY = _boxCollider.size.y;
        actualCenterY = _boxCollider.center.y;
        
        Physics.gravity = _gravity * new Vector3(0, -9.81f, 0);
    }

    // Update is called once per frame
    void Update()
    {
        
            if (Input.GetKeyDown(KeyCode.Space) && onGround && !_gameOver)
            {
                _rigidbody.AddForce(Vector3.up * jumpForce,ForceMode.Impulse);
                onGround = false;
                _animator.SetTrigger(JumpTrig);
                dirt.Stop();
                _audioSource.PlayOneShot(jumpClip, volumeClip);
                _boxCollider.size = new Vector3(_boxCollider.size.x, 2.428579f, _boxCollider.size.z);
                _boxCollider.center = new Vector3(_boxCollider.center.x, 1.71468f, _boxCollider.center.z);
                groundChecked = false;
            }

            if (Physics.Raycast(groundCheck.transform.position, Vector3.down, 0.1f, _layerMask) && !groundChecked)
            {
                _boxCollider.size = new Vector3(_boxCollider.size.x, actualSizeY, _boxCollider.size.z);
                _boxCollider.center = new Vector3(_boxCollider.center.x, actualCenterY, _boxCollider.center.z);
                groundChecked = true;
            }
    }
    
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Ground") && !_gameOver)
        {
            onGround = true;
            dirt.Play();
        }
        else if (other.gameObject.CompareTag("Obstacle"))
        {
            explosion.Play();
            dirt.Stop();
            _gameOver = true;
            _animator.SetBool(DeathB, true);
            _animator.SetInteger(DeathtypeINT,Random.Range(1,3));
            _audioSource.PlayOneShot(crashClip, volumeClip);
            Invoke("RestartScene", 2f);
        }
    }

    void RestartScene()
    {
        SceneManager.LoadScene("Prototype 3");
    }
}
