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

    public List<BoxInfo> boxes; // 박스 정보를 담을 List
    public TextMeshProUGUI displayText; // 텍스트를 표시할 UI 텍스트
    public float displayTime = 3f; // 텍스트를 표시할 시간

    private int currentIndex = 0; // 현재 박스의 인덱스
    private bool isCoroutineRunning = false; // 코루틴이 실행 중인지 여부를 나타내는 플래그

    void Start()
    {
        // 초기화 코드 등을 작성할 수 있습니다.
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
        isCoroutineRunning = true; // 코루틴이 실행 중임을 나타내는 플래그를 true로 설정

        if (currentIndex < boxes.Count)
        {
            // 현재 박스에 해당하는 텍스트를 표시
            displayText.text = boxes[currentIndex].displayText;
            displayText.gameObject.SetActive(true);

            // 일정 시간 동안 대기
            yield return new WaitForSeconds(displayTime);

            // 텍스트를 숨기고 다음 박스로 이동
            displayText.gameObject.SetActive(false);
            currentIndex++;

            // 다음 박스가 있으면 계속 진행, 없으면 초기화
            if (currentIndex < boxes.Count)
            {
                // 다음 박스로 이동
                Transform playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
                playerTransform.position = boxes[currentIndex].boxObject.transform.position;
            }
            else
            {
                // 모든 박스를 다 돌았을 때 초기화 또는 다른 작업 수행
                currentIndex = 0;
            }
        }

        isCoroutineRunning = false; // 코루틴이 종료되었음을 나타내는 플래그를 false로 설정
    }
}
