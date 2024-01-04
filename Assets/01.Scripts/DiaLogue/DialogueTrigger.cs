using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public Dialogue info;

    private void Start()
    {
        Invoke("Trigger", 1.7f);
    }

    public void Trigger()
    {
        var system = FindObjectOfType<DialogueSystem>();
        system.Begin(info);
    }
}
