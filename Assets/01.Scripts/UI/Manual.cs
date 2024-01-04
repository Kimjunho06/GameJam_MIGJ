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
        // ESC Ű�� ������ ��
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // Move ������Ʈ �Ǵ� Inter ������Ʈ�� Ȱ��ȭ ���̸�
            if (moveObject.activeSelf || interObject.activeSelf)
            {
                // Main ������Ʈ�� �̵�
                SwitchToMainObject();
            }
        }
    }

    void SwitchToMainObject()
    {
        // Main ������Ʈ�� Ȱ��ȭ�ϰ� ������ ������Ʈ�� ��Ȱ��ȭ
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
