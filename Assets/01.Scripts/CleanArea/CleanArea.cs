using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CleanArea : MonoBehaviour
{
    public int maxCnt;
    [SerializeField] private ObjectType objectType;
    [SerializeField] private List<GameObject> objects;

    [SerializeField] private Image objImg;
    [SerializeField] private Sprite objSprite;
    [SerializeField] private TextMeshProUGUI objCntTxt;

    private int cnt;

    public bool isClear;

    private void Start()
    {
        cnt = 0;

        if (maxCnt + 1 != objects.Count)
        {
            Debug.LogError("Not Enough Objects");
        }

        foreach(var obj in objects)
        {
            obj.SetActive(false);
        }

        objects[cnt].SetActive(true);
        cnt++;

        objImg.sprite = objSprite;
    }

    private void Update()
    {
        if (cnt == maxCnt + 1)
            isClear = true;

        objCntTxt.SetText($"{cnt - 1} / {maxCnt}");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Object>(out Object obj))
        {
            if (obj._objectType == objectType)
            {
                obj.gameObject.SetActive(false);

                foreach (var activeObj in objects)
                {
                    activeObj.gameObject.SetActive(false);
                }

                objects[cnt].SetActive(true);

                cnt++;
            }
        }
    }
}
