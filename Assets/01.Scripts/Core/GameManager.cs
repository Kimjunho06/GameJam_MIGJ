using DG.Tweening.Core.Easing;
using DG.Tweening;
using System;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoSingleton<GameManager>
{
    [SerializeField] private PoolingListSO _poolingList;
    [SerializeField] private Transform _poolingTrm;
    [SerializeField] private GameObject _fadeImage;
    [SerializeField] private Image fadeImage;
    [SerializeField] private float fadeTime;

    public GameObject _pushDirectionArrow;
    public Camera _mainCam;
    FadeManager fadeManager;

    private void Awake()
    {
        fadeManager = GetComponent<FadeManager>();
        PoolManager.Instance = new PoolManager(_poolingTrm);
        foreach (PoolingPair pair in _poolingList.list)
        {
            PoolManager.Instance.CreatePool(pair.prefab, pair.type, pair.count);
        }

        DOTween.Init(recycleAllByDefault: true, useSafeMode: true, LogBehaviour.Verbose).SetCapacity(400, 100);
        
        _mainCam = Camera.main;
        if (_mainCam == null)
            Debug.LogError("MainCam is not Found");
    }

    private void Start()
    {
        fadeManager.FadeIn();
    }
}