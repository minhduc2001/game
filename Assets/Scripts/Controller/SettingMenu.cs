using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingMenu : MonoBehaviour
{
    public AudioMixer mixer;
    public Button btnQuitSetting;
    public Button btnClose;
    public Button btnResume;
    const string mixer_sfx = "SFXMixer";
    const string mixer_music = "MusicMixer";
    
    private void Awake()
    {
        PlayerPrefs.SetFloat("sound", 0.5f);
        PlayerPrefs.SetFloat("music", 0.5f);
        PlayerPrefs.SetInt("isfullscreen", 1);
    }

    private void Start()
    {
        if (gameObject.tag == "setting")
        {
            btnQuitSetting.gameObject.active = true;
            btnResume.gameObject.active = false;
        } else
        {
            btnQuitSetting.gameObject.active = false;
            btnResume.gameObject.active = true;
        }

        btnClose.onClick.AddListener(closeSettingMenu);
        btnQuitSetting.onClick.AddListener(closeSettingMenu);
        btnResume.onClick.AddListener(closeSettingMenu);
    }

    public void setSound(float sound)
    {
        Debug.Log("Sound " + sound);
        mixer.SetFloat(mixer_sfx, sound);
        PlayerPrefs.SetFloat("sound", sound);
    }

    public void setMusic(float music)
    {
        mixer.SetFloat(mixer_music, music);
        PlayerPrefs.SetFloat("music", music);
    }

    public void setFullScreen(bool isFullScreen)
    {
        Debug.Log(isFullScreen);
        Screen.fullScreen = isFullScreen;
    }

    public void closeSettingMenu()
    {
        gameObject.SetActive(false);
    }
}
