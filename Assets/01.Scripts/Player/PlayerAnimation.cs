using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    readonly int _velocityHash = Animator.StringToHash("velocity");
    readonly int _jumpHash = Animator.StringToHash("isJump");
    readonly int _airHangHash = Animator.StringToHash("isAirHang");
    readonly int _pushHash = Animator.StringToHash("isPush");
    Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void MoveAnimation(int value)
    {
        _animator.SetInteger(_velocityHash, value);
    }
    
    public void JumpAnimation(bool value)
    {
        _animator.SetBool(_jumpHash, value);
    }
    
    public void AirHangAnimation(bool value)
    {
        _animator.SetBool(_airHangHash, value);
    }
    
    public void PushAnimation(bool value)
    {
        _animator.SetBool(_pushHash, value);
    }

}
