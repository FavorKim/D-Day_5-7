using UnityEngine;

public enum SFX // 효과음 종류를 ENUM으로 정의.
{
    /*
     * ※ 제안 사항: 현재 '공이 핀을 쳤을 때의 효과음'이 스트라이크일 때의 것으로 지정되어 있습니다.
     * 아래의 방법이 더 적절해 보이므로, 다음과 같이 수정해 주세요.
     * 1. 단일 효과음을 사용한다. 각 핀에 대해 이 효과음을 지정하여, 그 개수에 비례해 자연스럽게 중복된(겹치는 듯한) 효과음을 재생한다.
     */

    BALL_HIT,           // 공이 바닥에 떨어질 때
    BALL_ROLL,          // 공이 굴러갈 때
    GUTTER,             // 공이 거터에 빠졌을 때
    // PIN_HIT,         // 공이 핀을 쳤을 때 (단일) => 이것을 사용하도록 수정해 주세요.
    STRIKE,             // 공이 핀을 쳤을 때 (스트라이크) => 이것은 사용하지 않고, 위의 것으로 대체합니다.
    SPARE_CONGRATS,     // 스페어를 처리했을 때
    STRIKE_CONGRATS,    // 스트라이크를 처리했을 때
};

// 배경음, 효과음 등을 관리하는 매니저 클래스
public class AudioManager1 : MonoBehaviour
{
    // ※ 보다 더 적절한 방법으로 싱글톤을 구현하거나, 또는 싱글톤을 사용하지 않고 구현해 주세요.
    public static AudioManager1 instance; // 싱글톤으로 구현하기 위한 정적 인스턴스


    [Header("#BGM")] // 배경음
    private AudioSource bgmPlayer; // AudioSource 컴포넌트
    [SerializeField] private AudioClip bgmClip; // AudioClip
    [SerializeField] private float bgmVolume; // 음량

    [Header("#SFX")] // 효과음
    private AudioSource[] sfxPlayers; // AudioSource 컴포넌트
    [SerializeField] private AudioClip[] sfxClips; // AudioClip
    [SerializeField] private float sfxVolume; // 음량
    [SerializeField] private int channels; // 채널(음원 종류)
    [SerializeField] private int channelIndex; // 채널 번호(인덱스)


    private void Awake()
    {
        instance = this;
        Init();
    }

    private void Start()
    {
        PlayBGM(); // 게임 시작과 동시에 배경음을 재생한다.
    }


    private void Init() // 초기화
    {
        /*
         * ※ 이 코드는 실행 시 각 빈 게임 오브젝트를 자식으로 생성하고, AudioSource 컴포넌트를 부착하여 관리하고 있습니다.
         * 이 방법이 적절한지 다시 한 번 생각하고, 적절히 수정해 주세요.
         * 
         * 제안 사항: 현재 배경음으로 하나의 파일만을 사용하고 있습니다. (Music Tracks 폴더의 Zephyr 파일)
         * 이 에셋은 여러 개의 배경음을 제공하고 있으므로, 매번 랜덤하게 재생하거나 연속적으로 재생하는 등 다양하게 사용할 수 있도록 수정해 보아요.
         * 
         * 유의점: 배경음은 일반적으로 항상 하나의 파일만 재생되고, 효과음은 상황에 따라 종종 여러 개의 파일이 동시에 재생됩니다.
         */

        // 배경음
        GameObject bgmObject = new GameObject("BGM Player");
        bgmObject.transform.parent = transform;
        bgmPlayer = bgmObject.AddComponent<AudioSource>();
        bgmPlayer.playOnAwake = false;
        bgmPlayer.loop = true;
        bgmPlayer.volume = bgmVolume;
        bgmPlayer.clip = bgmClip;

        // 효과음
        GameObject sfxObject = new GameObject("SFX Player");
        sfxObject.transform.parent = transform;
        sfxPlayers = new AudioSource[channels];

        for (int index = 0; index < sfxPlayers.Length; index++)
        {
            sfxPlayers[index] = sfxObject.AddComponent<AudioSource>();
            sfxPlayers[index].playOnAwake = false;
            sfxPlayers[index].volume = sfxVolume;
        }
    }


    public void PlayBGM() // 배경음을 재생
    {
        bgmPlayer.Play();
    }

    public void StopBGM() // 배경음을 중지
    {
        bgmPlayer.Stop();
    }


    public void PlaySFX(SFX sfx) // 효과음을 실행(매개변수 enum 사용)
    {
        /*
         * ※ 개인적으로 조금 난해한 코드로 보입니다. 보다 간결하게 구현할 수 있을 것 같아요.
         */

        for (int index = 0; index < sfxPlayers.Length; index++)
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

    // 배경음 볼륨을 가져오는 메서드
    public float GetBgmVolume()
    {
        return bgmPlayer.volume;
    }

    // 배경음 볼륨을 설정하는 메서드
    public void SetBgmVolume(float volume)
    {
        bgmPlayer.volume = volume;
    }

    // 효과음 볼륨을 가져오는 메서드
    public float GetSfxVolume()
    {
        return sfxVolume;
    }

    // 효과음 볼륨을 설정하는 메서드
    public void SetSfxVolume(float volume)
    {
        sfxVolume = volume;
        foreach (var player in sfxPlayers)
        {
            player.volume = volume;
        }
    }
}
