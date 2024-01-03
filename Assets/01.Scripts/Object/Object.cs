using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(MessDisplay))]
public class Object : MonoBehaviour
{
    public float mess;

    [SerializeField] private Transform _groundChecker;
    [SerializeField] private float _groundCheckDistance = 0.51f;
    [SerializeField] private LayerMask _whatIsGround;

    [SerializeField] protected float _gravity = -9.8f;

    protected Rigidbody rb;

    public bool isPushed;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
    }

    /// <summary>
    /// true 밀릴 수 있음, false 밀리지 않음
    /// </summary>
    /// <param name="interactiveObj">때린 애</param>
    /// <param name="interactiedObj">맞는 애</param>
    public bool IsMessLarge(Object interactiveObj, Object interactiedObj)
    {
        if (interactiveObj.mess > interactiedObj.mess)
            return true;

        return false;
    }

    public bool IsGround()
    {
        return Physics.Raycast(_groundChecker.position, Vector3.down, _groundCheckDistance, _whatIsGround);
    }

    private void FixedUpdate()
    {
        if (!IsGround())
            rb.velocity += new Vector3(0, _gravity * Time.fixedDeltaTime, 0);
    }

    public void StopVelocity()
    {
        rb.velocity = Vector3.zero;
    }

    public void MoveAbleObject()
    {
        rb.constraints = RigidbodyConstraints.FreezeRotation;
    }
    
    public void MoveUnAbleObject()
    {
        rb.constraints = RigidbodyConstraints.FreezeAll;
    }

#if UNITY_EDITOR
    protected virtual void OnDrawGizmos()
    {
        if (_groundChecker != null)
            Gizmos.DrawLine(_groundChecker.position, _groundChecker.position + new Vector3(0, -_groundCheckDistance, 0));
    }
#endif
}
