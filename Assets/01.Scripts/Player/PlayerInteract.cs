using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class PlayerInteract : MonoBehaviour
{
    [SerializeField] private float _radius = 5f;
    [SerializeField] private float _hangCheckDist = 5f;
    [SerializeField] private Vector3 _findObjOffset;
    [SerializeField] private Transform hangCheckPos;

    Object interactableObj = null;
    Object hangableObj = null;
    Object playerObj;
    PlayerMovement playerMovement;

    private void Awake()
    {
        playerObj = GetComponent<Object>();
        playerMovement = playerObj.gameObject.GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        FindInteractableObject();
        FindHangableObject();

        PullObject();
        PushObject();

        AirHangObject();
    }

    private void PullObject()
    {
        if (Input.GetKey(KeyCode.Q))
        {
            if (interactableObj == null)
                return;
            if (!playerObj.IsMessLarge(playerObj, interactableObj))
                return;
            if (!playerMovement.IsGround())
                return;

            if (interactableObj.TryGetComponent<PullPushObject>(out PullPushObject obj))
            {
                if (!playerMovement.isStop)
                {
                    playerObj.StopVelocity();
                    playerMovement.isStop = true;
                }

                obj.PullObject(playerObj, interactableObj); // 당기기
                playerObj.gameObject.GetComponent<PlayerMovement>().isPull = true;
            }
        }
        if (Input.GetKeyUp(KeyCode.Q))
        {
            if (interactableObj == null)
                return;

            interactableObj.MoveUnAbleObject();
            playerObj.gameObject.GetComponent<PlayerMovement>().isPull = false;
            playerMovement.isStop = false;
        }
    }

    private void PushObject()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (interactableObj == null)
                return;
            if (!playerObj.IsMessLarge(playerObj, interactableObj))
                return;

            if (interactableObj.TryGetComponent<PullPushObject>(out PullPushObject obj))
            {
                obj.PushObject(playerObj, interactableObj);
            }
        }
    }

    private void AirHangObject()
    {

        if (Input.GetKey(KeyCode.F))
        {
            Vector3 hangPos = Vector3.zero;
            if (!interactableObj.TryGetComponent<HangableObject>(out HangableObject obj))
                return;
            else
                hangPos = obj._hangPos;

            playerMovement.isHang = true;

            playerMovement._playerAnimatior.AirHangAnimation(playerMovement.isHang);
            if (playerObj.IsGround())
            {
                playerMovement.isHang = false;
                playerMovement._playerAnimatior.AirHangAnimation(playerMovement.isHang);
                return;
            }
            playerObj.transform.position = hangPos;
            //DrainMess();
        }
        if (Input.GetKeyUp(KeyCode.F))
        {
            playerMovement.isHang = false;
            playerMovement._playerAnimatior.AirHangAnimation(playerMovement.isHang);
        }

    }

    public void DrainMess()
    {
        float minusMess = 0;
        if (!playerMovement.isHang)
            return;

        if (interactableObj.mess <= 0)
            return;

        if (!interactableObj.gameObject.TryGetComponent(out HangableObject obj))
        {
            Debug.LogError("HangableObj is null");
            return;
        }
        else
            minusMess = obj._minusMess;

        if (interactableObj.mess - minusMess < 0)
        {
            interactableObj.mess -= interactableObj.mess;
            playerObj.mess += interactableObj.mess;

            return;
        }

        interactableObj.mess -= minusMess;
        playerObj.mess += minusMess;
    }

    private void FindInteractableObject()
    {
        if (playerMovement.isPull) return;

        float maxDist = _radius + 1;
        bool isFindObj = false;
        
        Collider[] findObj = Physics.OverlapSphere(transform.position + _findObjOffset, _radius);

        foreach (var obj in findObj)
        {
            if (obj.TryGetComponent(out Object findInteractObj))
            {
                if (obj.gameObject == this.gameObject) continue;
                float dist = Mathf.Abs(Vector3.Distance(transform.position, findInteractObj.transform.position));

                if (dist < maxDist)
                {
                    maxDist = dist;
                    interactableObj = findInteractObj;
                    isFindObj = true;
                }
            }
        }

        if (interactableObj == null)
            return;

        // 범위 나갔을 때 안에 저장된 오브젝트 초기화.
        if (!isFindObj)
            interactableObj = null;
    }

    private void FindHangableObject()
    {
        RaycastHit hit;
        bool isFindObj = false;

        if (Physics.Raycast(hangCheckPos.position, playerObj.transform.forward, out hit, _hangCheckDist))
        {
            if (hit.collider == null) return;
            if (hit.collider.TryGetComponent<Object>(out Object obj))
            {
                if (obj.TryGetComponent<HangableObject>(out HangableObject hangable))
                {
                    hangableObj = obj;
                    isFindObj = true;
                }
            }
        }

        if (isFindObj)
            hangableObj = null;
    }

#if UNITY_EDITOR
    protected virtual void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position + _findObjOffset, _radius);

        if (playerObj != null)
            Gizmos.DrawRay(hangCheckPos.position, playerObj.transform.forward * _hangCheckDist);
    }
#endif
}
