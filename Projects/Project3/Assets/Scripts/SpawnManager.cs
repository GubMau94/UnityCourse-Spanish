using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject[] obstaclePrefabs;
    private int index;
    private float startDelay = 3f;
    private float repeatRate = 2f;

    private PlayerController _playerController;
    
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("Spawner", startDelay, repeatRate);
        _playerController = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    void Spawner()
    {
        if (!_playerController.GameOver)
        {
            index = Random.Range(0, obstaclePrefabs.Length);
            Instantiate(obstaclePrefabs[index], transform.position, obstaclePrefabs[index].transform.rotation);
        }
    }
    
}
