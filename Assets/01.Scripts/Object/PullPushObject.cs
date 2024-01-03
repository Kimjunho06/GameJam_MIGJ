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
        interactiedObj.MoveAbleObject();

        Vector3 dir = (transform.position - interactiveObj.transform.position).normalized;
        Vector3 objectPos = interactiveObj.transform.position;
        
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

        transform.position = objectPos + interactiedObj._pullOffset;
        interactiedObj.StopVelocity();
    }

    public void PushObject(Object interactiveObj, Object interactiedObj)
    {
        interactiedObj.isPushed = true;

        Component cmp = interactiedObj.GetComponentInParent<SwingObject>();
        if (cmp == null)
            interactiedObj.MoveAbleObject();

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

        interactiveObj.mess -= interactiedObj.mess;
        
        rb.AddForce(dir * 10, ForceMode.Impulse);

        StartCoroutine(UnableMoveDelay(interactiedObj));
        interactiedObj.isPushed = false;
    }

    IEnumerator UnableMoveDelay(Object obj)
    {
        WaitForSeconds time = new WaitForSeconds(0.1f);
        yield return time;

        while (rb.velocity != Vector3.zero)
        {
            yield return null;
        }

        obj.MoveUnAbleObject();

    }
}
