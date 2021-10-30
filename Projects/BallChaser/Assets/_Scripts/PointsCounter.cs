using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PointsCounter : MonoBehaviour
{

    private GameObject _enemy;
    private GameObject _player;

    private string MAX_POINTS = "POINTS";
    private int maxPoints;

    private int points = 0;

    private Text _pointsText;
    private Text _maxPointsText;


    // Start is called before the first frame update
    void Start()
    {
        _enemy = GameObject.FindGameObjectWithTag("Enemy");
        _player = GameObject.Find("Player");
        _pointsText = GameObject.Find("Points").GetComponent<Text>();
        _maxPointsText = GameObject.Find("MaxPoints").GetComponent<Text>();

        maxPoints = 0;
    }


    private void Update()
    {
        maxPoints = PlayerPrefs.GetInt(MAX_POINTS);

        _pointsText.text = "Points: " + points;
        _maxPointsText.text = "/ " + maxPoints;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            PlayerController.gold++;
            PlayerPrefs.SetInt(PlayerController.GOLD, PlayerController.gold);

            points++;
            if(maxPoints < points)
            {
                PlayerPrefs.SetInt(MAX_POINTS, points);
            }            
        }

        if (other.CompareTag("Player"))
        {
            points = 0;
        }
    }
}
