using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;


[RequireComponent(typeof(BoxCollider))]
public class RefreshBackground : MonoBehaviour
{
    [SerializeField]
    private BoxCollider _boxColliderOther;

    private float _pos;
    
    // Start is called before the first frame update
    void Start()
    {
        _pos = _boxColliderOther.size.x;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x < -80)
        {
            transform.position = new Vector3(_boxColliderOther.transform.position.x + _pos, 
                transform.position.y, transform.position.z);
        }
    }
}
