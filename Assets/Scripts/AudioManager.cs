using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BgmSound
{
    public AudioClip clip;
}

[System.Serializable]
public class SfxSound
{
    public AudioClip clip;
}

public class AudioManager : MonoBehaviour
{
    private static AudioManager instance;
    public static AudioManager Instance
    {
        get;
        private set;
    }

    [Header("배경음")]
    [SerializeField] BgmSound[] bgmsounds;

    [Header("브금 플레이어")]
    [SerializeField] AudioSource bgmPlayer;

    [Header("효과음")]
    [SerializeField] SfxSound[] sfxSounds;

    [Header("효과음 플레이어")]
    [SerializeField] AudioSource SfxPlayer;

    
    public enum Sfx { pin_hit, ball_roll,gutter};
    //공이 핀을 쳤을떄, 볼 굴러가는 중,거터에 빠졌을떄

    void Awake()
    {
       if(Instance!=null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Update()
    {
        if(!bgmPlayer.isPlaying) 
        {
            BgmPlay();
        }
    }

    public void BgmPlay() //bgm실행
    {
        int random = Random.Range(0, 9);
        bgmPlayer.clip = bgmsounds[random].clip;
        bgmPlayer.Play();
    }

    public void BgmStop() //bgm중단
    {
        bgmPlayer.Stop();
    }

    public void SfxPlay(Sfx sfx) //효과음 실행(매개변수 enum 사용)
    {
        SfxPlayer.clip = sfxSounds[(int)sfx].clip;
        SfxPlayer.Play();
    }
}

