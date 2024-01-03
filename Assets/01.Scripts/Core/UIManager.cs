using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.EventSystems;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject panel;
    [SerializeField] private GameObject newGamePanel;
    [SerializeField] private GameObject exitGamePanel;
    [SerializeField] private GameObject _fadeImage;
    [SerializeField] private Image fadeImage;
    [SerializeField] private float fadeTime;

    private bool isPanelOpened = true;
    private bool isFaded = false;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            OpenPanel();
        }
    }

    public void OpenPanel()
    {
        if(isPanelOpened == false)
        {
            Time.timeScale = 0f;
            panel.SetActive(true);
            isPanelOpened = true;
        }
        else if (isPanelOpened == true)
        {
            Time.timeScale = 1f;
            panel.SetActive(false);
            isPanelOpened = false;
        }
    }

    public void OnClickStart()
    {
        _fadeImage.SetActive(true);
        if (isFaded == false)
        {
            isFaded = true;
            fadeImage.DOFade(1, fadeTime).OnComplete(()=> isFaded = false);
        }
        Invoke("ChangeMain", fadeTime);
    }

    public void ChangeMain()
    {
        SceneManager.LoadScene("SampleSceneJiheon");
    }

    public void ChangeIntro()
    {
        SceneManager.LoadScene("IntroScene");
    }

    public void ChangeOption()
    {
        SceneManager.LoadScene("InGameOption");
    }

    public void OnClickExit()
    {
        exitGamePanel.SetActive(true);
        var btn = exitGamePanel.transform.Find("Image/TMP/Button/YesButton").GetComponent<Button>();
        btn.Select();
    }

    public void OnClickOption()
    {
        _fadeImage.SetActive(true);
        if (isFaded == false)
        {
            isFaded = true;
            fadeImage.DOFade(1, fadeTime);
            isFaded = false;
        }
        Invoke("ChangeOption", fadeTime);
    }

    public void RealNewGame()
    {
        _fadeImage.SetActive(true);
        if (isFaded == false)
        {
            isFaded = true;
            fadeImage.DOFade(1, fadeTime).OnComplete(() => isFaded = false);
        }
        Invoke("ChangeIntro", fadeTime);
    }
    
    public void RealExitGame()
    {
        Application.Quit();
    }

    public void OnClickBack()
    {
        Time.timeScale = 1f;
        panel.SetActive(false);
        isPanelOpened = false;
    }

    public void OnClickNo()
    {
        newGamePanel.SetActive(false);
        exitGamePanel.SetActive(false);
        var btn = panel.transform.Find("Line/GameOption/Button/BackButton").GetComponent<Button>();
        btn.Select();
    }

    public void OnClickNewGame()
    {
        newGamePanel.SetActive(true);
        var btn = newGamePanel.transform.Find("Image/TMP/Button/YesButton").GetComponent<Button>();
        btn.Select();
    }
}
