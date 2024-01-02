using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PullPushObject : MonoBehaviour
{
    Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void PullObject(Object interactiveObj, Object interactiedObj)
    {
        Vector3 dir = (transform.position - interactiveObj.transform.position).normalized;
        Vector3 objectPos = interactiveObj.transform.position;
        objectPos.y += 2f;
        
        if (Mathf.Abs(dir.x) > Mathf.Abs(dir.z))
        {
            if (dir.x > 0)
            {
                dir = new Vector3(0, 90, 0);
                objectPos += Vector3.right;
            }
            else
            {
                dir = new Vector3(0, 270, 0);
                objectPos += Vector3.left;
            }
        }
        else if (Mathf.Abs(dir.x) <= Mathf.Abs(dir.z))
        {
            if (dir.z > 0)
            {
                dir = new Vector3(0, 0, 0);
                objectPos += Vector3.forward;
            }
            else
            {
                dir = new Vector3(0, 180, 0);
                objectPos += Vector3.back;
            }
        }

        interactiveObj.transform.rotation = Quaternion.Euler(dir);

        transform.position = objectPos;
        interactiedObj.StopVelocity();
    }

    public void PushObject(Object interactiveObj, Object interactiedObj)
    {
        float calcMess = interactiveObj.mess - interactiedObj.mess;
        Vector3 dir = (interactiedObj.transform.position - interactiveObj.transform.position).normalized;

        if (Mathf.Abs(dir.x) > Mathf.Abs(dir.z))
        {
            if (dir.x > 0)
                dir = Vector3.right;
            else 
                dir = Vector3.left;
        }
        else if (Mathf.Abs(dir.x) <= Mathf.Abs(dir.z))
        {
            if (dir.z > 0)
                dir = Vector3.forward;
            else
                dir = Vector3.back;
        }

        interactiveObj.mess = 0;
        interactiedObj.mess = calcMess;

        rb.AddForce(dir * calcMess, ForceMode.Impulse);
    }
}
