using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderGraph;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Transform _groundChecker;
    [SerializeField] private float _moveSpeed = 3f;
    [SerializeField] private float _groundCheckDistance = 3f;

    [SerializeField] private LayerMask _whatIsGround;

    [SerializeField] private InputReader _inputReader;
    
    private Rigidbody rb;

    private float _gravity = -9.8f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        PlayerMove();
    }

    private void PlayerMove()
    {
        Vector3 dir = new Vector3(_inputReader.InputPos.x, 0, _inputReader.InputPos.y);
        dir *= _moveSpeed;

        if (!IsGround())
            dir.y += _gravity;

        rb.velocity = dir;
    }

    public bool IsGround()
    {
        return Physics.Raycast(_groundChecker.position, Vector3.down, _groundCheckDistance, _whatIsGround);
    }

#if UNITY_EDITOR
    protected virtual void OnDrawGizmos()
    {
        if (_groundChecker != null)
            Gizmos.DrawLine(_groundChecker.position, _groundChecker.position + new Vector3(0, -_groundCheckDistance, 0));
    }
#endif
}
