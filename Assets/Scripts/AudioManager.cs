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

    [Header("�����")]
    [SerializeField] BgmSound[] bgmsounds;

    [Header("��� �÷��̾�")]
    [SerializeField] AudioSource bgmPlayer;

    [Header("ȿ����")]
    [SerializeField] SfxSound[] sfxSounds;

    [Header("ȿ���� �÷��̾�")]
    [SerializeField] AudioSource SfxPlayer;

    
    public enum Sfx { pin_hit, ball_roll,gutter};
    //���� ���� ������, �� �������� ��,���Ϳ� ��������

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

    public void BgmPlay() //bgm����
    {
        int random = Random.Range(0, 9);
        bgmPlayer.clip = bgmsounds[random].clip;
        bgmPlayer.Play();
    }

    public void BgmStop() //bgm�ߴ�
    {
        bgmPlayer.Stop();
    }

    public void SfxPlay(Sfx sfx) //ȿ���� ����(�Ű����� enum ���)
    {
        SfxPlayer.clip = sfxSounds[(int)sfx].clip;
        SfxPlayer.Play();
    }
}

