using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Unity.VisualScripting;

public class DialogueSystem : MonoBehaviour
{
    public TextMeshProUGUI txtName;
    public TextMeshProUGUI txtSentence;
    public Button playerDialogue;
    public Button inforDialogue;
    public Vector3 dialogueVec;
    private bool isTyping = false;
    private bool isSecond = false;

    Queue<string> sentences = new Queue<string>();
    private string currentSentence;

    Coroutine typingCo;

    private void Awake()
    {
        dialogueVec = playerDialogue.transform.position;
    }

    private void Start()
    {
        StartLog();
    }

    public void Begin(Dialogue info)
    {
        sentences.Clear();

        txtName.text = info.name;

        foreach(var sentence in info.sentences)
        {
            sentences.Enqueue(sentence);
        }
        Next();
    }

    public void Next()
    {
        if(isTyping == true)
        {
            return;
        }
        if(sentences.Count == 0)
        {
            EndLog();
            return;
        }
        txtSentence.text = string.Empty;
        StopAllCoroutines();
        typingCo = StartCoroutine(Typing(sentences.Dequeue()));
    }

    public IEnumerator Skip()
    {
        StopCoroutine(typingCo);

        txtSentence.text = currentSentence;
        yield return new WaitForSeconds(0.1f);
        isSecond = false;
        isTyping = false;
    }

    private void Update()
    {
        if(isTyping == true && Input.GetMouseButtonDown(0) && isSecond) 
        {
            StartCoroutine(Skip());
        }
    }

    IEnumerator Typing(string sentence)
    {
        isSecond = true;
        isTyping = true;
        currentSentence = sentence;

        foreach(var letter in sentence)
        {
            txtSentence.text += letter;
            yield return new WaitForSeconds(0.08f);
        }
        
    }

    private void StartLog()
    {
        playerDialogue.transform.DOMoveY(240, 1.5f, false);
    }

    private void EndLog()
    {
        txtSentence.text = string.Empty;
        playerDialogue.transform.DOMoveY(-800, 1.5f, false);
    }
}