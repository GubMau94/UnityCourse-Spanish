using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnManagerX : MonoBehaviour
{
    public GameObject[] ballPrefabs;

    private float spawnLimitXLeft = -22;
    private float spawnLimitXRight = 7;
    private float spawnPosY = 30;
    
    private float spawnInterval = 3f;

    private float counter;

    private int index;

    // Start is called before the first frame update
    void Start()
    {
        Invoke("SpawnRandomBall", spawnInterval);
    }

    // Spawn random ball at random x position at top of play area
    void SpawnRandomBall ()
    {
        index = Random.Range(0, ballPrefabs.Length);
        
        // Generate random ball index and random spawn position
        Vector3 spawnPos = new Vector3(Random.Range(spawnLimitXLeft, spawnLimitXRight), spawnPosY, 0);

        // instantiate ball at random spawn location
        Instantiate(ballPrefabs[index], spawnPos, ballPrefabs[index].transform.rotation);

        Physics.gravity = new Vector3(0, Random.Range(-2.5f, -9.81f), 0);
        
        Invoke("SpawnRandomBall", spawnInterval);
    }

    private void Update()
    {
        counter += Time.deltaTime;
      //  Debug.Log("counter: " + counter);
        if (counter > spawnInterval)
        { 
            // Debug.Log("spawn interval " + spawnInterval);
            
            spawnInterval = Random.Range(1f, 5f);
            counter = 0f;
        }
    }
}
