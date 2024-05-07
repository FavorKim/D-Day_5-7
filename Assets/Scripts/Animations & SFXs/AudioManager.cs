using System.Collections;
using System.Collections.Generic;
using UnityEngine;




[System.Serializable] 
public class BgmSound //bgm����
{
    public AudioClip clip;
}

[System.Serializable]
public class SfxSound //ȿ���� ����
{
    public AudioClip clip;
}

public class AudioManager : MonoBehaviour
{

    private static AudioManager instance;
    public static AudioManager Instance //������Ƽ
    {
        get;
        private set;
    }

    [Header("�����")]
    [SerializeField] BgmSound[] bgmsounds;

    [Header("��� �÷��̾�")]
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
        if(!bgmPlayer.isPlaying)  //������ ���鼭 ���
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
}

