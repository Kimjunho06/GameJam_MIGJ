using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverObject : MonoBehaviour
{
    public MoveMentObject moveMentObject;
    public GameObject pivot;

    private void Update()
    {
        if (moveMentObject.isLever)
            pivot.transform.localRotation = Quaternion.Euler(90, 0, 0);
        else
            pivot.transform.localRotation = Quaternion.Euler(0, 0, 0);
    }

}
