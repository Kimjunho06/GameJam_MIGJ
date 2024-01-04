using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopObject : MonoBehaviour
{
    public float delayTime;
    Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        StartCoroutine(StopObjDelay(delayTime));   
    }
    

    IEnumerator StopObjDelay(float time)
    {
        yield return new WaitForSeconds(time);

        rb.constraints = RigidbodyConstraints.FreezeAll;
    }

}
