using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerX : MonoBehaviour
{
    public GameObject dogPrefab;

    private float counter = 0f;
    private float nextTime = 0.8f;

    // Update is called once per frame
    void Update()
    {
        counter += Time.deltaTime;

        if (counter > nextTime)
        {
            // On spacebar press, send dog
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Instantiate(dogPrefab, transform.position, dogPrefab.transform.rotation);
                counter = 0f;
            }
        }
        
    }
}
