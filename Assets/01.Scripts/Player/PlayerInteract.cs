using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    [SerializeField] private float _radius = 5f;
    [SerializeField] private Vector3 _findObjOffset;
    Object interactableObj = null;
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
        
        if (interactableObj == null)
        {
            Debug.Log("interactable Object is null");
            return;
        }

        PullObject();
        PushObject();

        AirHangObject();
    }

    private void PullObject()
    {
        if (Input.GetKey(KeyCode.Q))
        {
            if (!playerObj.IsMessLarge(playerObj, interactableObj))
                return;

            if (interactableObj.TryGetComponent<PullPushObject>(out PullPushObject obj))
            {
                obj.PullObject(playerObj, interactableObj); // ´ç±â±â
                playerObj.gameObject.GetComponent<PlayerMovement>().isPull = true;
            }
        }
        if (Input.GetKeyUp(KeyCode.Q))
            playerObj.gameObject.GetComponent<PlayerMovement>().isPull = false;
    }

    private void PushObject()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
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
            DrainMess();
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

        if (!isFindObj)
            interactableObj = null;
    }

#if UNITY_EDITOR
    protected virtual void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position + _findObjOffset, _radius);
    }
#endif
}
