using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MoveMentObject : Object
{
    [SerializeField] private GameObject _point1;
    [SerializeField] private GameObject _point2;

    bool isCheck = false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        Vector3 dir = Vector3.zero;
        if (isCheck)
            dir = _point1.transform.position - transform.position;
        else
            dir = _point2.transform.position - transform.position;
        dir.y = 0;

        rb.velocity = dir;    
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Point"))
            isCheck = !isCheck;
    }
}
