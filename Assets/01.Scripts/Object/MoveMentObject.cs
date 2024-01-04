using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MoveMentObject : MonoBehaviour
{
    public bool isLever = false;

    [SerializeField] private GameObject _point1;
    [SerializeField] private GameObject _point2;

    [SerializeField] private float _speed;

    [SerializeField] private float time;


    private float curTime;
    private Rigidbody rb;
    bool isCheck = false;
    bool isStay = false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        curTime = 0;
    }

    private void Update()
    {
        // point1 up
        // point2 down
        if (!isLever)
            return;

        Vector3 dir = Vector3.zero;
        if (isCheck)
            dir = _point1.transform.position - transform.position;
        else
            dir = _point2.transform.position - transform.position;

        rb.MovePosition(rb.position + dir * _speed * Time.deltaTime);

        if (isStay)
        {
            curTime += Time.deltaTime;
            if (curTime > time) 
            {
                isCheck = true;
                curTime = 0;
                isStay = false;
            }
        }

        //rb.velocity = dir * _speed;    
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == _point1)
        {
            isLever = false;
            isCheck = false;
        }
        
        if (other.gameObject == _point2)
        {
            isStay = true;
        }
    }


}
