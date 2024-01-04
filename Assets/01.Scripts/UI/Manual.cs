using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manual : MonoBehaviour
{
    public GameObject mainObject;
    public GameObject moveObject;
    public GameObject interObject;

    void Update()
    {
        // ESC 키를 눌렀을 때
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // Move 오브젝트 또는 Inter 오브젝트가 활성화 중이면
            if (moveObject.activeSelf || interObject.activeSelf)
            {
                // Main 오브젝트로 이동
                SwitchToMainObject();
            }
        }
    }

    void SwitchToMainObject()
    {
        // Main 오브젝트를 활성화하고 나머지 오브젝트는 비활성화
        mainObject.SetActive(true);
        moveObject.SetActive(false);
        interObject.SetActive(false);
    }

    public void MoveButton()
    {
        mainObject.SetActive(false);
        moveObject.SetActive(true);
        interObject.SetActive(false);
    }

    public void InterButton()
    {
        mainObject.SetActive(false);
        moveObject.SetActive(false);
        interObject.SetActive(true);
    }
}
