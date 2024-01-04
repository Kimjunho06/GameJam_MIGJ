using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using TreeEditor;
using UnityEditor.ShaderGraph;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : Object
{
    [SerializeField] private float _moveSpeed = 3f;
    [SerializeField] private float _rotateSpeed = 3f;
    [SerializeField] private float _jumpForce = 3f;

    [SerializeField] private float _deadPos = -5f;

    [SerializeField] private InputReader _inputReader;
    public InputReader InputReader => _inputReader;

    [SerializeField] private CinemachineFreeLook _playerCam;

    public PlayerAnimation _playerAnimatior;

    public bool isPull;
    public bool isAir = false;
    public bool isHang = false;
    public bool isPush = false;

    public bool isStop = false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        _playerAnimatior = GetComponentInChildren<PlayerAnimation>();
    }

    private void Start()
    {
        _inputReader.JumpEvent += OnJumpHandle;
    }

    private void OnJumpHandle()
    {
        if (IsGround())
        {
            // 점프 시 y 방향 속도를 설정
            rb.velocity = new Vector3(rb.velocity.x, Mathf.Sqrt(2f * _jumpForce * Mathf.Abs(_gravity)), rb.velocity.z);
        }
    }

    private void Update()
    {
        if (transform.position.y < _deadPos)
        {
            transform.position = Vector3.zero;
            rb.velocity = Vector3.zero;
        }

        if (!IsGround())
            isAir = true;
        if (IsGround())
            isAir = false;

        if (isPush)
            StopVelocity();

        _playerAnimatior.JumpAnimation(isAir);
        //_playerCam.m_XAxis.Value += _inputReader.InputPos.x * _rotateSpeed * Time.deltaTime;

        //transform.rotation = Quaternion.Euler(0, GameManager.Instance._mainCam.transform.rotation.eulerAngles.y, 0);
        
    }

    private void FixedUpdate()
    {
        if (isPush)
        {
            _playerAnimatior.MoveAnimation(0);
            return;
        }
        PlayerMove();
    }

    private void PlayerMove()
    {
        if (isPull)
        {
            if (_inputReader.InputPos.y > 0 || _inputReader.InputPos.x != 0)
                return;

            Vector3 backDir = transform.forward * _inputReader.InputPos.y;
            backDir.y = 0;
            backDir.Normalize();

            backDir *= _moveSpeed;
            rb.velocity = backDir;
            _playerAnimatior.MoveAnimation((int)Mathf.Abs(_inputReader.InputPos.y));
            return;
        }

        Camera mainCam = GameManager.Instance._mainCam;

        Vector3 cameraForward = mainCam.transform.forward;
        cameraForward.y = 0;
        cameraForward.Normalize();
        
        Vector3 cameraRight = mainCam.transform.right;
        cameraRight.y = 0;
        cameraRight.Normalize();
        
        //Vector3 dir = new Vector3(_inputReader.InputPos.x, 0, _inputReader.InputPos.y);
        Vector3 dir = cameraForward * _inputReader.InputPos.y + cameraRight * _inputReader.InputPos.x;
        dir.Normalize();
        dir *= _moveSpeed;

        _playerAnimatior.MoveAnimation((int)(Mathf.Abs(_inputReader.InputPos.x) + Mathf.Abs(_inputReader.InputPos.y)));

        rb.velocity = new Vector3(dir.x, rb.velocity.y, dir.z);

        if (dir != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(dir, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, Time.fixedDeltaTime * 1000f);
        }
    }

    
}
