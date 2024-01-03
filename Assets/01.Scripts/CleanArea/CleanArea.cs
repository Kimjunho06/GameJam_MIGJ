using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CleanArea : MonoBehaviour
{
    public int maxCnt;
    [SerializeField] private ObjectType objectType;
    [SerializeField] private List<GameObject> objects;
    
    private int cnt;

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
