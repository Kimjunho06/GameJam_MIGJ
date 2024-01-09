using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageController : MonoBehaviour
{
    public string curSceneName;
    public string NextSceneName;

    public List<CleanArea> cleanArea;

    public List<CleanAreaH> cleanAreaH;

    private void Start()
    {
        if (curSceneName == "EndingScene")
        {
            GameManager.Instance.CursorOn();
        }
    }

    private void Update()
    {
        if (curSceneName == "EndingScene")
        {
            GameManager.Instance.CursorOn();
            return;
        }
            StageClear();
        StageClearH();

        if (Input.GetKeyDown(KeyCode.B))
        {
            ResetStage();
        }

        if (Input.GetKeyDown(KeyCode.L))
            NextStage();
    }

    public void ResetStage()
    {
        SceneManager.LoadScene(curSceneName);
    }

    public void StageClear()
    {
        int cnt = 0;
        foreach(var check in cleanArea)
        {
            if (check.isClear)
            {
                cnt++;
            }

            if (cnt == cleanArea.Count)
                NextStage();
        }
    }
    
    public void StageClearH()
    {
        int cnt = 0;
        foreach (var check in cleanAreaH)
        {
            if (check.isClear)
            {
                cnt++;
            }

            if (cnt == cleanAreaH.Count)
                NextStage();
        }
    }

    public void NextStage()
    {
        SceneManager.LoadScene(NextSceneName);
    }
}
