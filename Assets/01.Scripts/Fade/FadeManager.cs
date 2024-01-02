using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class FadeManager : MonoBehaviour
{
    [SerializeField] private Image fadeImage;
    [SerializeField] private float fadeTime;
    private bool isFaded = true;

    void Start()
    {
        FadeIn();
    }

    private void FadeIn()
    {
        if (isFaded == true)
        {
            isFaded = false;
            fadeImage.DOFade(0, fadeTime).OnComplete(() => isFaded = true);
        }
    }

    private void FadeOut()
    {
        if (isFaded == false)
        {
            isFaded = true;
            fadeImage.DOFade(0, fadeTime).OnComplete(() => isFaded = false);
        }
    }
}
