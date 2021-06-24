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

    public int _difficultyLevel;

    private float spawnRate = 1f;

    public enum GameState
    {
        loading,
        inGame,
        GameOver
    }
    public GameState _gameState;

    private int _score;
    private int Score
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

    private void Start()
    {
        UpdateMaxScore();
    }

    /// <summary>
    /// Inizia il giorno quando lo stato è "inGame"
    /// </summary>
    public void StartGame(int difficulty)
    {
        _difficultyLevel = difficulty;
        _gameState = GameState.inGame;
        difficultPanel.gameObject.SetActive(false);
        spawnRate /= difficulty;
        StartCoroutine(SpawnTarget());

        Score = 0;
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
        Score += _score;
        scoreText.text = "" + Score;
    }

    private const string MAX_SCORE = "MAX_SCORE";

    private void UpdateMaxScore()
    {
        int maxScore = PlayerPrefs.GetInt(MAX_SCORE, 0);
        scoreText.text = "" + maxScore;
    }

    private void SetMaxScore()
    {
        int maxScore = PlayerPrefs.GetInt(MAX_SCORE, 0);
        if (Score > maxScore)
        {
            PlayerPrefs.SetInt(MAX_SCORE, Score);

        }
    }

    /// <summary>
    /// Attiva il pannello di Game Over
    /// </summary>
    public void GameOver()
    {
        SetMaxScore();

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
