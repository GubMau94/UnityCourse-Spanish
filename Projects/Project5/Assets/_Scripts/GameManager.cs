using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    [SerializeField] private List<GameObject> targetPrefabs;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private GameObject gameOverPanel, difficultPanel;

    private float spawnRate = 1f;

    public enum GameState
    {
        loading,
        inGame,
        GameOver
    }
    public GameState _gameState;


    private int _score;
    private int score
    {
        set
        {
            _score = Mathf.Clamp(value, 0, 99999);
        }
        get
        {
            return _score;
        }
    }

    /// <summary>
    /// Inizia il giorno quando lo stato è "inGame"
    /// </summary>
    public void StartGame(int difficulty)
    {
        _gameState = GameState.inGame;
        difficultPanel.gameObject.SetActive(false);
        spawnRate /= difficulty;
        StartCoroutine(SpawnTarget());

        score = 0;
        UpdateScore(0);
    }

    IEnumerator SpawnTarget()
    {
        while (_gameState == GameState.inGame)
        {
            int index = Random.Range(0, targetPrefabs.Count);
            yield return new WaitForSeconds(spawnRate);
            Instantiate(targetPrefabs[index]);
        }
        
    }

    /// <summary>
    /// Aggiorna il valore dei punti e lo pubblica sullo schermo
    /// </summary>
    /// <param name="_score"></param>
    public void UpdateScore(int _score)
    {
        score += _score;
        scoreText.text = "" + score;
    }

    /// <summary>
    /// Attiva il pannello di Game Over
    /// </summary>
    public void GameOver()
    {
        _gameState = GameState.GameOver;
        gameOverPanel.gameObject.SetActive(true);
    }

    /// <summary>
    /// Reinizia la scena attualmente attiva
    /// </summary>
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

}
