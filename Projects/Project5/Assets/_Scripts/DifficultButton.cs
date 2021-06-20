using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DifficultButton : MonoBehaviour
{
    private Button button;
    private GameManager gameManager;

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
        gameManager.StartGame();
    }
}
