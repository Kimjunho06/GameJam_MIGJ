using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 3f;

    [SerializeField] private InputReader InputReader = null;
    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        PlayerMove();
    }

    private void PlayerMove()
    {
        rb.velocity = new Vector3(InputReader.InputPos.x, 0, InputReader.InputPos.y);
    }
}
