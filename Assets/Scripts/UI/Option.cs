using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Option : MonoBehaviour
{
    RectTransform rect;


    Slider musicAudioSlider;
    Slider cfxAudioSlide;

     void Awake()
    {

        rect = GetComponent<RectTransform>();
        Button cancel = transform.Find("Cancel_Button").GetComponent<Button>();
        cancel.onClick.AddListener(Close);

        musicAudioSlider = transform.Find("BGMSlider").GetComponent<Slider>();
        musicAudioSlider.minValue = 0.0001f;
        musicAudioSlider.maxValue = 1.0f;
        

        cfxAudioSlide = transform.Find("CFXSlider").GetComponent<Slider>();
        cfxAudioSlide.minValue = 0.0001f;
        cfxAudioSlide.maxValue = 1.0f;

        Button exit = transform.Find("ExitButton").GetComponent<Button>();
        exit.onClick.AddListener(GameQuit);

        

        musicAudioSlider.onValueChanged.AddListener(MusicAudioChange);
        cfxAudioSlide.onValueChanged.AddListener(CfxAudioChange);
    }

    private void Start()
    {
        musicAudioSlider.value = GameManager.Instance.MusicVolume;
        cfxAudioSlide.value = GameManager.Instance.CfxVolume;
        GameManager.Instance.audioMixer.SetFloat("Music", Mathf.Log10(GameManager.Instance.MusicVolume) * 20);
        GameManager.Instance.audioMixer.SetFloat("Animal", Mathf.Log10(GameManager.Instance.CfxVolume) * 20);

        Close();
    }

    private void MusicAudioChange(float arg0)
    {
        GameManager.Instance.MusicVolume = arg0;
       GameManager.Instance.audioMixer.SetFloat("Music", Mathf.Log10( arg0)*20);
    }

    void CfxAudioChange(float arg0)
    {
        GameManager.Instance.CfxVolume = arg0;
        GameManager.Instance.audioMixer.SetFloat("Animal", Mathf.Log10(arg0) * 20);
    }

    public void Open()
    {
        gameObject.SetActive(true);
        rect.anchoredPosition = Vector2.zero;

    }

    public void Close()
    {
        gameObject.SetActive(false);
    }

    void GameQuit()
    {
        Debug.Log("게임종료");
        Application.Quit();
    }


  
}
