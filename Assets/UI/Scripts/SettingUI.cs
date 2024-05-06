using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingUI : MonoBehaviour
{

    public Slider bgmSlider;
    public Slider sfxSlider;

    private void Start()
    {
        // 배경음 볼륨 초기화
        bgmSlider.value = AudioManager1.instance.GetBgmVolume();
        // 효과음 볼륨 초기화
        sfxSlider.value = AudioManager1.instance.GetSfxVolume();
    }

    public void SetBgmVolume(float volume)
    {
        AudioManager1.instance.SetBgmVolume(volume);
    }

    public void SetSfxVolume(float volume)
    {
        AudioManager1.instance.SetSfxVolume(volume);
    }

}
