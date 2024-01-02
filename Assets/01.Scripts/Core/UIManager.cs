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
    [SerializeField] private Image fadeImage;
    [SerializeField] private float fadeTime;

    private bool isPanelOpened = false;
    private bool isFaded = false;

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
        if (isFaded == false)
        {
            isFaded = true;
            fadeImage.DOFade(1, fadeTime).OnComplete(()=> isFaded = false);
        }
        Invoke("ChangeScene", 1);
    }

    public void ChangeScene()
    {
        SceneManager.LoadScene("SampleScene");

    }

    public void OnClickExit()
    {
        exitGamePanel.SetActive(true);
    }

    public void OnClickOption()
    {
        if (isFaded == false)
        {
            isFaded = true;
            fadeImage.DOFade(1, fadeTime);
            isFaded = false;
        }
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
        var btn = newGamePanel.transform.Find("Image/TMP/Button/YesButton").GetComponent<Button>();
        btn.Select();
    }
}
