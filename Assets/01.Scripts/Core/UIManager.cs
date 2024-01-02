using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject panel;
    [SerializeField] private GameObject newGamePanel;
    [SerializeField] private GameObject exitGamePanel;
    [SerializeField] private Image fadeImage;
    [SerializeField] private float fadeTime;

    private bool isPanelOpened = false;
    private bool isFaded = false;
    private bool isCoroutined = false;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            OpenPanel();
        }        
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            OnClickBack();
        }
    }

    public void OpenPanel()
    {
        if(isPanelOpened == false)
        {
            panel.SetActive(true);
            isPanelOpened = true;
        }
        else if (isPanelOpened == true)
        {
            panel.SetActive(false);
            isPanelOpened = false;
        }
    }

    public void OnClickStart()
    {
        StartCoroutine(FadeCoroutine(0.01f));
        if(isCoroutined == false)
        {
            SceneManager.LoadScene("SampleScene");
        }
    }

    public void OnClickExit()
    {
        exitGamePanel.SetActive(true);
    }

    public void OnClickOption()
    {
        StartCoroutine(FadeCoroutine(0.01f));
        SceneManager.LoadScene("OptionScene");
    }

    public void RealNewGame()
    {
        SceneManager.LoadScene("IntroScene");
    }
    
    public void RealExitGame()
    {
        Application.Quit();
    }

    public void OnClickBack()
    {
        panel.SetActive(false);
        isPanelOpened = false;
    }

    public void OnClickNo()
    {
        newGamePanel.SetActive(false);
        exitGamePanel.SetActive(false);
    }

    public void OnClickNewGame()
    {
        newGamePanel.SetActive(true);
    }

    IEnumerator FadeCoroutine(float fadeInOut)
    {
        isCoroutined = true;
        float fadeCnt = 0;
        if(isFaded == false)
        {
            isFaded = true;
            fadeImage.DOFade(1, fadeTime);
            yield return null;
            //while (fadeCnt < 1.0f)
            //{
            //    fadeCnt += fadeInOut;
            //    yield return new WaitForSeconds(0.01f);
            //    fadeImage.color = new Color(0, 0, 0, fadeCnt);
            //}
            isFaded = false;
        }
        isCoroutined = false;
    }
}
