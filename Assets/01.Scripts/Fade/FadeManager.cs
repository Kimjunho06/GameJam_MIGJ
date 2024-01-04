using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class FadeManager : MonoBehaviour
{
    [SerializeField] private GameObject _fadeImage;
    [SerializeField] private Image fadeImage;
    [SerializeField] private float fadeTime;
    private bool isFaded = false;

    void Start()
    {
        FadeIn();
    }

    public void FadeIn()
    {
        _fadeImage.SetActive(true);
        if (isFaded == false)
        {
            isFaded = true;
            fadeImage.DOFade(0, fadeTime).OnComplete(() => isFaded = false);
        }
        Invoke("OffFade", 1);
    }


    private void OffFade()
    {
        _fadeImage.SetActive(false);
    }
}
