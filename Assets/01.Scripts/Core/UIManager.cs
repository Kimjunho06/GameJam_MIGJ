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
    [SerializeField] private Button backButton;
    [SerializeField] private Button newGameButton;
    [SerializeField] private Button exitButton1;
    [SerializeField] private Button exitButton2;
    [SerializeField] private Button startButton;
    [SerializeField] private Button tutorialButton;
    [SerializeField] private Button explainButton;
    [SerializeField] private Button restartButton;

    private bool isPanelOpened = false;
    private bool isFaded = false;

    private Button selectedButton;

    private void Awake()
    {
        selectedButton = restartButton;
    }
    private void Start()
    {
        //selectedButton.Select();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            OpenPanel();
            selectedButton.Select();
        }


        if (selectedButton == backButton && Input.GetKeyDown(KeyCode.DownArrow))
        {
            selectedButton = newGameButton;
        }
        else if (selectedButton == newGameButton && Input.GetKeyDown(KeyCode.DownArrow))
        {
            selectedButton = exitButton2;
        }
        if (selectedButton == newGameButton && Input.GetKeyDown(KeyCode.UpArrow))
        {
            selectedButton = backButton;
        }
        if (selectedButton == exitButton2 && Input.GetKeyDown(KeyCode.UpArrow))
        {
            selectedButton = newGameButton;
        }
 
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
            selectedButton = exitButton1;
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
            if (explainButton == null)
            {
                selectedButton = exitButton1;
            }
        }

        if (selectedButton == exitButton1 && Input.GetKeyDown(KeyCode.UpArrow))
        {
            selectedButton = explainButton;
            if (explainButton == null)
            {
                selectedButton = restartButton;
            }
        }
    }

    public void OpenPanel()
    {
        selectedButton = backButton;
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
            fadeImage.DOFade(1, fadeTime).OnComplete(()=> isFaded = false).SetUpdate(true);
            Time.timeScale = 1f;
        }
        Invoke("ChangeMain", fadeTime);
    }

    public void RealNewGame()
    {
        _fadeImage.SetActive(true);
        if (isFaded == false)
        {
            isFaded = true;
            fadeImage.DOFade(1, fadeTime).OnComplete(() => isFaded = false).SetUpdate(true);
            Time.timeScale = 1f;
        }
        Invoke("ChangeIntro", fadeTime);
    }
    public void OnClickExit()
    {
        exitGamePanel.SetActive(true);
        Debug.Log("d");
        var btn = exitGamePanel.transform.Find("Image/TMP/Button/YesButton").GetComponent<Button>();
        btn.Select();
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
        SceneManager.LoadScene("Tutorial");
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
