using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndingPanel : MonoBehaviour
{
    public DialogueSystem dialogueSystem;
    public GameObject panel;

    void Start()
    {
        panel.SetActive(false);
    }

    void Update()
    {
        if(dialogueSystem.isEnd == true)
        {
            panel.SetActive(true);
        }
    }
}
