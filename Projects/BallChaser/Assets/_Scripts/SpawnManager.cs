using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject[] enemyPrefab;
    [SerializeField] private GameObject[] powerPrefab;
    private float spawnPosX, spawnPosZ;
    private float spawnLimit = 9f;

    private int wave = 1;
    private int enemyCount;
    private int timer;
    private int newWaveSpawnTimer = 60;

    private int nextEnemy = 5;
    private int enemyType = 1;

    private bool startNewWave;

    private Text _waveText;
    private Text _countdownText;
    
    public enum GameState
    {
        loading,
        inGame,
        GameOver
    }
    public GameState gameState;

    public void StartGame()
    {
        gameState = GameState.inGame;
        StartCoroutine(EnemySpawn(wave));
        _waveText = GameObject.Find("Wave").GetComponent<Text>();
        _countdownText = GameObject.Find("Countdown").GetComponent<Text>();

        StartCoroutine("NextWaveCountdown");
    }


    void Update()
    {
        NewWave();
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

    /// <summary>
    /// Genera un nuovo round quando i nemici sono pari a zero
    /// </summary>
    private void NewWave()
    {
        if(gameState == GameState.inGame)
        {
            enemyCount = GameObject.FindGameObjectsWithTag("Enemy").Length;
            if (enemyCount == 0 || startNewWave)
            {
                StopCoroutine("NextWaveCountdown");
                StartCoroutine("NextWaveCountdown");

                StopCoroutine(StartNewWave());

                int index = Random.Range(0, powerPrefab.Length);

                wave++;
                StartCoroutine(EnemySpawn(wave));

                int numPowers = Random.Range(0, 3);
                for (int i = 0; i < numPowers; i++)
                {
                    Instantiate(powerPrefab[index], GenerateSpawnPos(), powerPrefab[index].transform.rotation);
                }

                startNewWave = false;
                _waveText.text = "Wave: " + wave;
            }
        }
    }

    /// <summary>
    /// Genera un numero di nemici pari all'ondata attuale
    /// </summary>
    /// <param name="numberOfEnemies"></param>
    /// <returns></returns>
    IEnumerator EnemySpawn(int numberOfEnemies)
    {
        for (int i = 0; i < numberOfEnemies; i++)
        {            
            int enemyLevel = Random.Range(0, enemyType);

            Instantiate(enemyPrefab[enemyLevel], GenerateSpawnPos(), enemyPrefab[enemyLevel].transform.rotation);
            yield return new WaitForSeconds(0.5f);

            //Fa in modo di aumentare la difficolt? aggiungendo nuove tipologie di enemici
            if (wave >= nextEnemy)
            {                
                enemyType++;
                nextEnemy *= 2;
                if(enemyType > 5)
                {
                    enemyType = 5;
                }
            }
        }

        StartCoroutine(StartNewWave());
    }

    /// <summary>
    /// Genera una nuova ondata se entro 60s non vengono buttati tutti i nemici fuori dall'isola
    /// </summary>
    /// <returns></returns>
    IEnumerator StartNewWave()
    {
        yield return new WaitForSeconds(newWaveSpawnTimer);
        startNewWave = true;
        NewWave();        
    }

    IEnumerator NextWaveCountdown()
    {
        timer = newWaveSpawnTimer;
        while (timer > 0)
        {
            yield return new WaitForSeconds(1f);
            timer--;
            _countdownText.text = "" + timer;
        }
    }

} //class
