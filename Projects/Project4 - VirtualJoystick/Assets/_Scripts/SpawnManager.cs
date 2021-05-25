using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private GameObject powerPrefab;
    private float spawnPosX, spawnPosZ;
    private float spawnLimit = 9f;

    private int wave = 1;
    private int enemyCount;
    

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(EnemySpawn(wave));
    }

    void Update()
    {
        enemyCount = GameObject.FindGameObjectsWithTag("Enemy").Length;
        if(enemyCount == 0)
        {
            wave++;
            StartCoroutine(EnemySpawn(wave));
            Instantiate(powerPrefab, GenerateSpawnPos(), powerPrefab.transform.rotation);
        }
    }


    /// <summary>
    /// Genera una posiziona aleatoria in X e Z
    /// </summary>
    /// <returns></returns>
    private Vector3 GenerateSpawnPos()
    {
        spawnPosX = Random.Range(-spawnLimit, spawnLimit);
        spawnPosZ = Random.Range(-spawnLimit, spawnLimit);
        Vector3 spawnRandomPos = new Vector3(spawnPosX, 0, spawnPosZ);
        return spawnRandomPos;
    }


    IEnumerator EnemySpawn(int numberOfEnemies)
    {
        for (int i = 0; i < numberOfEnemies; i++)
        {
            Instantiate(enemyPrefab, GenerateSpawnPos(), enemyPrefab.transform.rotation);
            yield return new WaitForSeconds(0.5f);
        }
       
    }
}
