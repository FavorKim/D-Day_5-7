using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BgmSound
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
}

