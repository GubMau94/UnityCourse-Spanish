using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowMouse : MonoBehaviour
{
    private TrailRenderer _trail;
    private Vector3 mousePos;
    [SerializeField] private GameObject _particlesPrefabs;
    private GameObject particlesClone;
    public int particlesCount;

    private bool mousePressed, destroyEnable, countEnable = true;


    private void Start()
    {
        _trail = GetComponent<TrailRenderer>();

    }

    // Update is called once per frame
    void Update()
    {
        particlesClone = GameObject.FindGameObjectWithTag("Particle");
        if (!destroyEnable && !mousePressed && particlesCount != 0)
        {
            countEnable = false;
            destroyEnable = true;
            StartCoroutine("DestroyParticleSystem");
        }


        MousePosition();
        MousePressed();
    }

    /// <summary>
    /// Mi converte la posizione del mouse in world space
    /// </summary>
    void MousePosition()
    {
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos = new Vector3(mousePos.x, mousePos.y);
        transform.position = mousePos;
    }
    
    /// <summary>
    /// Verifica se il mouse è premuto per instanziare un particle effect
    /// </summary>
    void MousePressed()
    {
        if (Input.GetMouseButtonDown(0))
        {
            mousePressed = true;
            StartCoroutine("SpawnEffect");
        }
        if (Input.GetMouseButtonUp(0))
        {
            mousePressed = false;
            StopCoroutine("SpawnEffect");
        }

        //Abilito il TrailRenderer se il mouse sinistro è premuto
        if (Input.GetMouseButton(0))
        {
            _trail.enabled = true;
            if (countEnable)
            {
                particlesCount = GameObject.FindGameObjectsWithTag("Particle").Length;
            }
            
        }
        else
        {
            _trail.enabled = false;
        }
    }

    private IEnumerator SpawnEffect()
    {
        while (mousePressed)
        {
            Instantiate(_particlesPrefabs, transform.position, _particlesPrefabs.transform.rotation);
            yield return new WaitForSeconds(0.05f);
        }
    }

    private IEnumerator DestroyParticleSystem()
    {
        yield return new WaitForSeconds(0.1f);
        for(int i = 0; i < particlesCount; i++)
        {
            Destroy(particlesClone);
        }
        countEnable = true;
        destroyEnable = false;
        
    }

}
