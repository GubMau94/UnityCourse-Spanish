using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PointsCounter : MonoBehaviour
{

    private GameObject _enemy;
    private GameObject _player;

    private int points = 0;

    private Text _pointsText;


    // Start is called before the first frame update
    void Start()
    {
        _enemy = GameObject.FindGameObjectWithTag("Enemy");
        _player = GameObject.Find("Player");
        _pointsText = GameObject.Find("Points").GetComponent<Text>();
    }


    private void Update()
    {
        _pointsText.text = "Points: " + points;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            points++;
        }

        if (other.CompareTag("Player"))
        {
            points = 0;
        }
    }
}
