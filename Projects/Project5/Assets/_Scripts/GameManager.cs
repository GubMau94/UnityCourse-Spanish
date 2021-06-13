using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    [SerializeField] List<GameObject> targetPrefabs;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnTarget());
    }

    IEnumerator SpawnTarget()
    {
        while (0 < 1)
        {
            int index = Random.Range(0, targetPrefabs.Count);
            yield return new WaitForSeconds(0.5f);
            Instantiate(targetPrefabs[index]);
        }
        
    }
}
