using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject[] enemiesPrefabs;
    private int index;
    [SerializeField] private float spawnBounds = 14f;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            index = Random.Range(0, enemiesPrefabs.Length);
            if (index <= enemiesPrefabs.Length)
            {
                Instantiate(enemiesPrefabs[index], 
                    new Vector3(Random.Range(-spawnBounds, spawnBounds), transform.position.y, transform.position.z),
                    enemiesPrefabs[index].transform.rotation);
            }
            else
            {
                Debug.Log("Index sbagliato tato");
            }
        }
    }
}
