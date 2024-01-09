using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class TutoralNextScene : MonoBehaviour
{
    public GameObject triggerObject; // 텍스트를 표시할 트리거 오브젝트
    public TextMeshProUGUI[] texts; // 여러 개의 텍스트를 저장할 배열
    public float textInterval = 2f; // 각 텍스트 간의 간격
    public float sceneTransitionDelay = 3f; // 텍스트가 모두 나온 후 다음 씬으로 전환하기까지의 대기 시간

    private int currentTextIndex = 0;

    void Start()
    {
        StartCoroutine(ShowTexts());
    }

    IEnumerator ShowTexts()
    {
        // 트리거 오브젝트가 활성화되어 있을 때만 진행
        while (triggerObject.activeSelf && currentTextIndex < texts.Length)
        {
            texts[currentTextIndex].gameObject.SetActive(true); // 현재 인덱스의 텍스트를 활성화
            yield return new WaitForSeconds(textInterval); // 일정 시간 동안 기다림
            texts[currentTextIndex].gameObject.SetActive(false); // 현재 인덱스의 텍스트를 비활성화
            currentTextIndex++; // 다음 텍스트로 이동
        }

        yield return new WaitForSeconds(sceneTransitionDelay); // 모든 텍스트가 나온 후 대기
        // 다음 씬으로 전환하는 코드를 여기에 추가하면 됩니다.
        SceneManager.LoadScene("IntroScene");
    }
}
