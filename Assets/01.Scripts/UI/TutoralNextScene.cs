using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class TutoralNextScene : MonoBehaviour
{
    public GameObject triggerObject; // �ؽ�Ʈ�� ǥ���� Ʈ���� ������Ʈ
    public TextMeshProUGUI[] texts; // ���� ���� �ؽ�Ʈ�� ������ �迭
    public float textInterval = 2f; // �� �ؽ�Ʈ ���� ����
    public float sceneTransitionDelay = 3f; // �ؽ�Ʈ�� ��� ���� �� ���� ������ ��ȯ�ϱ������ ��� �ð�

    private int currentTextIndex = 0;

    void Start()
    {
        StartCoroutine(ShowTexts());
    }

    IEnumerator ShowTexts()
    {
        // Ʈ���� ������Ʈ�� Ȱ��ȭ�Ǿ� ���� ���� ����
        while (triggerObject.activeSelf && currentTextIndex < texts.Length)
        {
            texts[currentTextIndex].gameObject.SetActive(true); // ���� �ε����� �ؽ�Ʈ�� Ȱ��ȭ
            yield return new WaitForSeconds(textInterval); // ���� �ð� ���� ��ٸ�
            texts[currentTextIndex].gameObject.SetActive(false); // ���� �ε����� �ؽ�Ʈ�� ��Ȱ��ȭ
            currentTextIndex++; // ���� �ؽ�Ʈ�� �̵�
        }

        yield return new WaitForSeconds(sceneTransitionDelay); // ��� �ؽ�Ʈ�� ���� �� ���
        // ���� ������ ��ȯ�ϴ� �ڵ带 ���⿡ �߰��ϸ� �˴ϴ�.
        SceneManager.LoadScene("IntroScene");
    }
}
