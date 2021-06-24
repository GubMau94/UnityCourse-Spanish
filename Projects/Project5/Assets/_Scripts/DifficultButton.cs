using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DifficultButton : MonoBehaviour
{
    private Button button;
    private GameManager gameManager;

    [Range(1, 3)]
    public int _difficult;
    private int Difficulty
    {
        get
        {
            return _difficult;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        button = GetComponent<Button>();
        gameManager = FindObjectOfType<GameManager>();
        button.onClick.AddListener(SetDifficult);
    }

    void SetDifficult()
    {
        Debug.Log("La difficoltà selezionata è: " + gameObject.name);
        gameManager.StartGame(Difficulty);
    }
}
