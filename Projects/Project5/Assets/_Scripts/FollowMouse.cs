using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowMouse : MonoBehaviour
{
    private TrailRenderer _trail;

    private void Start()
    {
        _trail = GetComponent<TrailRenderer>();
    }


    // Update is called once per frame
    void Update()
    {
        MousePosition();
    }

    void MousePosition()
    {
        if (Input.GetMouseButton(0))
        {
            _trail.enabled = true;
        }
        else
        {
            _trail.enabled = false;
        }

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            transform.position = new Vector3(hit.point.x, hit.point.y);
        }
    }

}
