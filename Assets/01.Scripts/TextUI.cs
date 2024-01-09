using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextUI : MonoBehaviour
{
    [System.Serializable]
    public class BoxInfo
    {
        public GameObject boxObject;
        public string displayText;
    }

    public List<BoxInfo> boxes; // �ڽ� ������ ���� List
    public TextMeshProUGUI displayText; // �ؽ�Ʈ�� ǥ���� UI �ؽ�Ʈ
    public float displayTime = 3f; // �ؽ�Ʈ�� ǥ���� �ð�

    private int currentIndex = 0; // ���� �ڽ��� �ε���
    private bool isCoroutineRunning = false; // �ڷ�ƾ�� ���� ������ ���θ� ��Ÿ���� �÷���

    void Start()
    {
        // �ʱ�ȭ �ڵ� ���� �ۼ��� �� �ֽ��ϴ�.
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isCoroutineRunning)
        {
            StartCoroutine(DisplayTextAndDisappear());
        }
    }

    IEnumerator DisplayTextAndDisappear()
    {
        isCoroutineRunning = true; // �ڷ�ƾ�� ���� ������ ��Ÿ���� �÷��׸� true�� ����

        if (currentIndex < boxes.Count)
        {
            // ���� �ڽ��� �ش��ϴ� �ؽ�Ʈ�� ǥ��
            displayText.text = boxes[currentIndex].displayText;
            displayText.gameObject.SetActive(true);

            // ���� �ð� ���� ���
            yield return new WaitForSeconds(displayTime);

            // �ؽ�Ʈ�� ����� ���� �ڽ��� �̵�
            displayText.gameObject.SetActive(false);
            currentIndex++;

            // ���� �ڽ��� ������ ��� ����, ������ �ʱ�ȭ
            if (currentIndex < boxes.Count)
            {
                // ���� �ڽ��� �̵�
                Transform playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
                playerTransform.position = boxes[currentIndex].boxObject.transform.position;
            }
            else
            {
                // ��� �ڽ��� �� ������ �� �ʱ�ȭ �Ǵ� �ٸ� �۾� ����
                currentIndex = 0;
            }
        }

        isCoroutineRunning = false; // �ڷ�ƾ�� ����Ǿ����� ��Ÿ���� �÷��׸� false�� ����
    }
}
