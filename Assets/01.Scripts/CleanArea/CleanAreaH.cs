using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CleanAreaH : MonoBehaviour
{
    public int maxCnt;
    [SerializeField] private ObjectType objectType;
    [SerializeField] private List<GameObject> objects;
    
    private int cnt;

    public bool isClear;

    private void Start()
    {
        cnt = 0;

        if (maxCnt != objects.Count)
        {
            Debug.LogError("Not Enough Objects");
        }
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
