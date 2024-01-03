using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HangableObject : MonoBehaviour
{
    public float _minusMess;
    public Vector3 _hangPos;

    private void Update()
    {
        _hangPos = transform.position;
    }
}
