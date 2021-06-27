using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Countdown : MonoBehaviour
{
    private SpawnManager _spawnManager;

    private void Start()
    {
        _spawnManager = FindObjectOfType<SpawnManager>();
    }

    void StartGameAfterCountdown()
    {
        _spawnManager.StartGame();
    }
}
