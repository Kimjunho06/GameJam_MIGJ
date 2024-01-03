using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwingObject : MonoBehaviour
{
    Object obj;

    public float power = 0;

    private float angle = 0;
    private float lerpTime = 0;
    private float speed = 2f;

    private void Awake()
    {
        obj = GetComponentInChildren<Object>();
    }

    private void Update()
    {
        angle = obj.mess * power; 
        lerpTime += Time.deltaTime * speed;
        transform.rotation = CalculateMovementOfPendulum();
    }

    Quaternion CalculateMovementOfPendulum()
    {
        return Quaternion.Lerp(Quaternion.Euler(Vector3.forward * angle),
            Quaternion.Euler(Vector3.back * angle), GetLerpTParam());
    }

    float GetLerpTParam()
    {
        return (Mathf.Sin(lerpTime) + 1) * 0.5f;
    }
}
