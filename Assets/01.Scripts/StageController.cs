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

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            ResetStage();
        }
    }

    public void ResetStage()
    {
        SceneManager.LoadScene(curSceneName);
    }

    public void StageClear()
    {
        foreach(var check in cleanArea)
        {
            if (!check.isClear)
                return;

            NextStage();
        }
    }
    
    public void StageClearH()
    {
        foreach (var check in cleanAreaH)
        {
            if (!check.isClear)
                return;

            NextStage();
        }
    }

    public void NextStage()
    {
        SceneManager.LoadScene(NextSceneName);
    }
}
