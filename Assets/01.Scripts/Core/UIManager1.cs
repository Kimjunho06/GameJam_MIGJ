using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.EventSystems;
using UnityEngine.Rendering;

public class UIManager1 : MonoBehaviour
{
    [SerializeField] private GameObject _fadeImage;
    [SerializeField] private Image fadeImage;
    [SerializeField] private float fadeTime;
    [SerializeField] private Button startButton;
    [SerializeField] private Button tutorialButton;
    [SerializeField] private Button explainButton;
    [SerializeField] private Button exitButton;
    [SerializeField] private Button restartButton;

    private bool isFaded = false;

    private Button selectedButton;

    private void Awake()
    {
        if(startButton != null)
        {
            selectedButton = startButton;
        }
        else
        {
            selectedButton = restartButton;
        }
    }

    private void Update()
    {
        selectedButton.Select();

        if (selectedButton == startButton && Input.GetKeyDown(KeyCode.DownArrow))
        {
            selectedButton = tutorialButton;
        }
        else if (selectedButton == tutorialButton && Input.GetKeyDown(KeyCode.DownArrow))
        {
            selectedButton = explainButton;
        }
        else if (selectedButton == explainButton && Input.GetKeyDown(KeyCode.DownArrow))
        {
            selectedButton = exitButton;
        }

        if (selectedButton == explainButton && Input.GetKeyDown(KeyCode.UpArrow))
        {
            selectedButton = tutorialButton;
            if (startButton == null)
            {
                selectedButton = restartButton;
            }
        }
        else if (selectedButton == tutorialButton && Input.GetKeyDown(KeyCode.UpArrow))
        {
            selectedButton = startButton;
        }

        if (selectedButton == restartButton && Input.GetKeyDown(KeyCode.DownArrow))
        {
            selectedButton = explainButton;
            if(explainButton == null)
            {
                selectedButton = exitButton;
            }
        }

        if(selectedButton == exitButton && Input.GetKeyDown(KeyCode.UpArrow))
        {
            selectedButton = explainButton;
            if (explainButton == null)
            {
                selectedButton = restartButton;
            }
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

    public void OnClickTutorial()
    {
        _fadeImage.SetActive(true);
        if (isFaded == false)
        {
            isFaded = true;
            fadeImage.DOFade(1, fadeTime);
            isFaded = false;
        }
        Invoke("ChangeTutorial", fadeTime);
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
    
    public void ChangeMain()
    {
        SceneManager.LoadScene("Stage 1");
    }

    public void ChangeIntro()
    {
        SceneManager.LoadScene("IntroScene");
    }

    public void ChangeTutorial()
    {
        SceneManager.LoadScene("SettingScene");
    }    
    
    public void ChangeExplain()
    {
        SceneManager.LoadScene("UIMovePanel");
    }

    public void RealExitGame()
    {
        Application.Quit();
    }
}
