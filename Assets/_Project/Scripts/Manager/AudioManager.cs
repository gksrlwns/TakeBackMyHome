using System.Collections;
using UnityEngine;

public enum SFX { Soldier_Fire, Soldier_Create, Soldier_Die, Soldier_Remove, Upgrade, Upgrade_None, StartGame, GameOver_Success, GameOver_Fail, ZombieSpawn}

public class AudioManager : MonoBehaviour
{
    static AudioManager instance;
    public static AudioManager Instance { get { return instance; } }

    [Header("BGM")]
    [SerializeField] AudioClip bgmClip;
    public AudioSource bgmPlayer;
    [Header("SFX")]
    [SerializeField] GameObject sfxObject;
    [SerializeField] AudioClip[] sfxClips;
    [SerializeField] int channels;
    public AudioSource[] sfxPlayers;
    int channelIndex;
    bool isFire;
    private float fireSoundCooldown = 0.3f;  // Soldier_Fire 사운드의 재생 주기
    private float lastFireSoundTime = -1f;


    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
        InitializeObject();
    }
    
    void InitializeObject()
    {
        bgmPlayer.playOnAwake = true;
        bgmPlayer.loop = true;
        bgmPlayer.clip = bgmClip;

        sfxPlayers = new AudioSource[channels];

        for(int i = 0; i < sfxPlayers.Length; i++)
        {
            sfxPlayers[i] = sfxObject.AddComponent<AudioSource>();
            sfxPlayers[i].playOnAwake = false;
            sfxPlayers[i].loop = false;
        }
    }
    public void PlayBGM(bool isPlay)
    {
        if (isPlay) bgmPlayer.Play();
        else bgmPlayer.Stop();
    }
    public void PlaySFX(SFX sfx)
    {
        for(int i = 0; i< sfxPlayers.Length;i++)
        {
            int loopIndex = (i + channelIndex) % sfxPlayers.Length;
            if (sfxPlayers[i].isPlaying) continue;

            channelIndex = loopIndex;
            sfxPlayers[loopIndex].clip = sfxClips[(int)sfx];

            // 어떻게 플레이 되도록 할지 고민
            if (sfx == SFX.Soldier_Fire)
            {
                if (Time.time - lastFireSoundTime < fireSoundCooldown) return;

                sfxPlayers[loopIndex].Play();
                lastFireSoundTime = Time.time;
            }
            else
            {
                sfxPlayers[loopIndex].Play();
            }
            break;

        }
    }
    
    IEnumerator WaitFire()
    {
        yield return CoroutineManager.DelaySeconds(1f);
        isFire = false;
    }
    
}
