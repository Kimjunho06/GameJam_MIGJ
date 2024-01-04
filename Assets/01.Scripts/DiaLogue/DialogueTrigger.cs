using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueTrigger : MonoBehaviour
{
    public Dialogue info;
    public Button playerDialogue;

    private void Start()
    {
        playerDialogue.GetComponent<Button>().interactable = false;
        Invoke("Trigger", 1.7f);
    }

    public void Trigger()
    {
        var system = FindObjectOfType<DialogueSystem>();
        system.Begin(info);
        playerDialogue.GetComponent<Button>().interactable = true;
    }
}
