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
        interactiveObj.GetComponent<PlayerMovement>().isPush = true;

        Component cmp = interactiedObj.GetComponentInParent<SwingObject>();
        if (cmp == null)
            interactiedObj.MoveAbleObject();

        Vector3 pos = (interactiedObj.transform.position - interactiveObj.transform.position).normalized;

        Vector3 dir = Vector3.zero;

        if (Mathf.Abs(pos.x) > Mathf.Abs(pos.z))
        {
            if (pos.x > 0)
            {
                pos = Vector3.right;
                dir = new Vector3(0, 90, 0);
            }
            else
            {
                pos = Vector3.left;
                dir = new Vector3(0, 270, 0);
            }
        }
        else if (Mathf.Abs(pos.x) <= Mathf.Abs(pos.z))
        {
            if (pos.z > 0)
            {
                pos = Vector3.forward;
                dir = new Vector3(0, 0, 0);
            }
            else
            {
                pos = Vector3.back;
                dir = new Vector3(0, 180, 0);
            }
        }

        interactiveObj.mess -= interactiedObj.mess;

        rb.AddForce(pos * interactiedObj.mess, ForceMode.Impulse);
        
        interactiveObj.transform.rotation = Quaternion.Euler(dir);

        StartCoroutine(UnableMoveDelay(interactiveObj, interactiedObj));
        interactiedObj.isPushed = false;
    }

    IEnumerator UnableMoveDelay(Object interactiveObj ,Object interactiedObj)
    {
        WaitForSeconds time = new WaitForSeconds(0.1f);
        yield return time;

        while (new Vector3((int)rb.velocity.x, (int)rb.velocity.y, (int)rb.velocity.z)!= Vector3.zero)
        {
            Debug.Log("stay");
            Debug.Log(rb.velocity);
            yield return null;
        }

        interactiedObj.MoveUnAbleObject();
        interactiveObj.GetComponent<PlayerMovement>().isPush = false;
    }
}
