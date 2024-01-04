using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverObject : MonoBehaviour
{
    public MoveMentObject moveMentObject;
    public GameObject pivot;

    public GameObject door;

    private void Update()
    {
        if (moveMentObject.isLever)
        {
            door.SetActive(false);
            pivot.transform.localRotation = Quaternion.Euler(90, 0, 0);
        }
        else
        {
            door.SetActive(true);
            pivot.transform.localRotation = Quaternion.Euler(0, 0, 0);
        }
    }

}
