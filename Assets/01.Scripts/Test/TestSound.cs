using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestSound : MonoBehaviour
{
    [SerializeField] private Slider bgmSlider;
    [SerializeField] private Slider sfxSlider;
    [SerializeField] private AudioSource bgmPlayer;
    [SerializeField] private AudioSource sfxPlayer;


    private void Awake()
    {
        bgmSlider = GetComponent<Slider>();
        sfxSlider = GetComponent<Slider>();
        bgmPlayer = GetComponent<AudioSource>();
        sfxPlayer = GetComponent<AudioSource>();
    }

    private void Start()
    {
        AudioManager.Instance.Play(bgmPlayer.clip, SoundEnum.BGM);
    }

    public void ChangeBGMSound(float value)
    {
        bgmPlayer.volume = value;
        sfxPlayer.volume = value;
    }
}
