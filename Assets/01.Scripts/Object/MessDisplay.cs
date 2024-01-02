using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class MessDisplay : MonoBehaviour
{
    [SerializeField] int _fontSize = 5;
    [SerializeField] Color _textColor = new Color(0, 0, 0, 100);
    [SerializeField] float _textYPos = 1f;
    private TextMeshPro messTxt;

    private void Awake()
    {
        messTxt = GetComponentInChildren<TextMeshPro>();
        if (messTxt == null)
        {
            GameObject go = new GameObject();
            go.AddComponent<TextMeshPro>();
            go.transform.SetParent(this.transform);

            messTxt = GetComponentInChildren<TextMeshPro>();
        }

        if (messTxt == null)
            Debug.LogError("messTxt is not Add");
    }

    private void Start()
    {
        messTxt.rectTransform.sizeDelta = new Vector2(0, 0);
        messTxt.rectTransform.anchoredPosition3D = new Vector3(0, _textYPos, 0);
        messTxt.fontSize = _fontSize;
        messTxt.color = _textColor;
        messTxt.alignment = TextAlignmentOptions.Center;
        messTxt.enableWordWrapping = false;
        messTxt.name = "messTxt";

        if (TryGetComponent<Object>(out Object obj))
            messTxt.text = $"{obj.mess.ToString("F1")}";
    }

    private void Update()
    {
        if (TryGetComponent<Object>(out Object obj))
            messTxt.text = $"{obj.mess.ToString("F1")}";

        messTxt.rectTransform.rotation = Quaternion.Euler(0, GameManager.Instance._mainCam.transform.rotation.y, 0);
    }
}
