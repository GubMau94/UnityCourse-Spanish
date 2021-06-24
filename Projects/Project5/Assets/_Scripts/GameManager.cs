using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    [SerializeField] private List<GameObject> targetPrefabs, lifeNumber;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private GameObject gameOverPanel, difficultPanel;

    private const string MAX_SCORE = "MAX_SCORE";

    [HideInInspector]
    public int _difficultyLevel;
    private int startingLifes = 4;

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

    private void ShowLifes(int NumberOfLifes)
    {
            for (int i = 0; i < NumberOfLifes; i++)
            {
                lifeNumber[i].SetActive(true);
            }
    }

    private void UpdateLifesLeft()
    {
        if(startingLifes >= 0)
        {
            Image heartImage = lifeNumber[startingLifes].GetComponent<Image>();
            var tempColor = heartImage.color;
            tempColor.a = 0.3f;
            heartImage.color = tempColor;
        }
    }

    /// <summary>
    /// Inizia il gioco quando lo stato è "inGame"
    /// </summary>
    public void StartGame(int difficulty)
    {
        _difficultyLevel = difficulty;
        _gameState = GameState.inGame;
        difficultPanel.gameObject.SetActive(false);
        spawnRate /= difficulty;
        startingLifes -= _difficultyLevel;
        ShowLifes(startingLifes);
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

        startingLifes--;
        UpdateLifesLeft();

        if(startingLifes <= 0)
        {
            _gameState = GameState.GameOver;
            gameOverPanel.gameObject.SetActive(true);
        }

    }

    /// <summary>
    /// Reinizia la scena attualmente attiva
    /// </summary>
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

}
