using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    [Header("#BGM")] //�����
    public AudioClip bgmClip;
    public float bgmVolume;
    AudioSource bgmPlayer;

    [Header("#SFX")] //ȿ����
    public AudioClip[] sfxClips;
    public float sfxVolume;
    public int channels;
    AudioSource[] sfxPlayers;
    int channelIndex;

    public enum Sfx {ball_hit,ball_roll,gutter,SpareCongrats,strike1,StrikeCongrats};
    //�� �ٴ� ��������,�� ��������,���� ���Ϳ� ��������, ����� ó�� ����,���� �� ������,��Ʈ����ũ ����

    void Awake()
    {
        instance = this;
        Init();
    }

    void Init() //�ʱ�ȭ
    {
        //�����
        GameObject bgmObject = new GameObject("BgmPlayer");
        bgmObject.transform.parent = transform;
        bgmPlayer = bgmObject.AddComponent<AudioSource>();
        bgmPlayer.playOnAwake = false;
        bgmPlayer.loop = true;
        bgmPlayer.volume = bgmVolume;
        bgmPlayer.clip = bgmClip;

        //ȿ����
        GameObject sfxObject = new GameObject("SfxPlayer");
        sfxObject.transform.parent = transform;
        sfxPlayers = new AudioSource[channels];

        for (int index = 0; index < sfxPlayers.Length; index++)
        {
            sfxPlayers[index] = sfxObject.AddComponent<AudioSource>();
            sfxPlayers[index].playOnAwake = false;
            sfxPlayers[index].volume = sfxVolume;
        }
    }

    void Start()
    {
        BgmPlay();
    }
    public void BgmPlay() //bgm����
    {
        bgmPlayer.Play();
    }

    public void BgmStop() //bgm�ߴ�
    {
        bgmPlayer.Stop();
    }

    public void SfxPlay(Sfx sfx) //ȿ���� ����(�Ű����� enum ���)
    {
        for(int index=0; index<sfxPlayers.Length; index++)
        {
            int loopIndex = (index + channelIndex) % sfxPlayers.Length;

            if (sfxPlayers[loopIndex].isPlaying)
            {
                continue;
            }

           
            channelIndex = loopIndex;
            sfxPlayers[0].clip = sfxClips[(int)sfx];
            sfxPlayers[0].Play();
            break;
        }

        
    }
}

