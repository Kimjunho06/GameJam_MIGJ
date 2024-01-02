using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    [SerializeField] private float _radius = 5f;
    Object interactableObj = null;
    Object playerObj;

    private void Awake()
    {
        playerObj = GetComponent<Object>();
    }

    private void Update()
    {
        FindInteractableObject();

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

    private void FindInteractableObject()
    {
        float maxDist = _radius + 1;

        Collider[] findObj = Physics.OverlapSphere(transform.position, _radius);

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
                }
            }
        }

        if (interactableObj == null)
            return;
    }

#if UNITY_EDITOR
    protected virtual void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, _radius);
    }
#endif
}
