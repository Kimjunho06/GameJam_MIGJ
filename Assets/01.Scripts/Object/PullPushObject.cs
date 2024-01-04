using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PullPushObject : MonoBehaviour
{
    [SerializeField] private float _pullDistance;

    [SerializeField] private float _moveDist;
    [SerializeField] private float _moveTime;

    Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void PullObject(Object interactiveObj, Object interactiedObj, Vector3 playerStartPos, Vector3 objStartPos)
    {
        interactiedObj.MoveAbleObject();

        bool isReset = false;
        if (Input.GetKeyDown(KeyCode.T))
        {
            interactiedObj.transform.position = objStartPos;
            isReset = true;
        }

        if (Vector3.Distance(objStartPos, interactiedObj.transform.position) >= _pullDistance || isReset)
        {
            if (interactiveObj.TryGetComponent<PlayerInteract>(out PlayerInteract interact))
            {
                interact.isPulling = false;
            }

            return;
        }

        Vector3 dir = (transform.position - interactiveObj.transform.position).normalized;
        Vector3 objectPos = interactiveObj.transform.position;
        
        if (Mathf.Abs(dir.x) > Mathf.Abs(dir.z))
        {
            if (dir.x > 0)
            {
                dir = new Vector3(0, 90, 0);
                //objectPos += Vector3.right;
            }
            else
            {
                dir = new Vector3(0, 270, 0);
                //objectPos += Vector3.left;
            }
        }
        else if (Mathf.Abs(dir.x) <= Mathf.Abs(dir.z))
        {
            if (dir.z > 0)
            {
                dir = new Vector3(0, 0, 0);
                //objectPos += Vector3.forward;
            }
            else
            {
                dir = new Vector3(0, 180, 0);
                //objectPos += Vector3.back;
            }
        }

        interactiveObj.transform.rotation = Quaternion.Euler(dir);

        //transform.position = objectPos + interactiedObj._pullOffset;
        Vector3 objOffset = objStartPos - playerStartPos;
        Vector3 pos = objectPos + objOffset;
        
        pos.y = 0;
        
        interactiedObj.transform.position = pos + interactiedObj._pullOffset;

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

        //rb.AddForce(pos * interactiedObj.mess, ForceMode.Impulse);

        Vector3 movePos = interactiedObj.transform.position;
        movePos += pos * _moveDist;

        interactiveObj.transform.rotation = Quaternion.Euler(dir);

        interactiedObj.transform.DOMove(movePos, _moveTime / interactiedObj.mess);

        StartCoroutine(UnableMoveDelay(interactiveObj, interactiedObj));
        interactiedObj.isPushed = false;
    }

    IEnumerator UnableMoveDelay(Object interactiveObj ,Object interactiedObj)
    {
        WaitForSeconds time = new WaitForSeconds(0.1f);
        yield return time;

        while (new Vector3((int)rb.velocity.x, (int)rb.velocity.y, (int)rb.velocity.z)!= Vector3.zero)
        {
            yield return null;
        }

        interactiedObj.MoveUnAbleObject();
        interactiveObj.GetComponent<PlayerMovement>().isPush = false;
    }
}
