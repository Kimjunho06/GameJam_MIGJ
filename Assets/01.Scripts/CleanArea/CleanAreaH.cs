using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CleanAreaH : MonoBehaviour
{
    public int maxCnt;
    [SerializeField] private ObjectType objectType;
    [SerializeField] private List<GameObject> objects;
    
    private int cnt;

    [SerializeField] private Image objImg;
    [SerializeField] private Sprite objSprite;
    [SerializeField] private TextMeshProUGUI objCntTxt;

    public bool isClear;

    private void Start()
    {
        cnt = 0;

        if (maxCnt != objects.Count)
        {
            Debug.LogError("Not Enough Objects");
        }

        objImg.sprite = objSprite;
    }

    private void Update()
    {
        if (maxCnt == cnt)
            isClear = true;

        objCntTxt.SetText($"{cnt} / {maxCnt}");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Object>(out Object obj))
        {
            if (obj._objectType == objectType)
            {
                obj.gameObject.SetActive(false);

                objects[cnt].SetActive(true);

                cnt++;
            }
        }
    }
}
