using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    readonly int _velocityhash = Animator.StringToHash("velocity");
    Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void MoveAnimation(int value)
    {
        _animator.SetInteger(_velocityhash, value);
    }

}
