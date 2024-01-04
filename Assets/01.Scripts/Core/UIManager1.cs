using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.EventSystems;

public class UIManager1 : MonoBehaviour
{
    [SerializeField] private GameObject _fadeImage;
    [SerializeField] private Image fadeImage;
    [SerializeField] private float fadeTime;
    [SerializeField] private Button startButton;
    [SerializeField] private Button optionButton;
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

        if(selectedButton == startButton && Input.GetKeyDown(KeyCode.DownArrow))
        {
            selectedButton = exitButton;
        }
        if (selectedButton == exitButton && Input.GetKeyDown(KeyCode.UpArrow))
        {
            selectedButton = startButton;
            if(startButton == null)
            {
                selectedButton = restartButton;
            }
        }

        if(selectedButton == restartButton && Input.GetKeyDown(KeyCode.DownArrow))
        {
            selectedButton = exitButton;
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
    
    public void Restart()
    {
        SceneManager.LoadScene("IntroScene");
    }

    public void RealExitGame()
    {
        Application.Quit();
    }
}
