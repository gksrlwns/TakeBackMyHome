using System.Collections;
using UnityEngine;
using UnityEngine.Audio;

public enum SFX { Soldier_Fire, Soldier_Create, Soldier_Die_Obstacle, Soldier_Remove_Count, Upgrade, Upgrade_None, StartGame, GameOver_Success, GameOver_Fail, ZombieSpawn, Button_Click}

public class AudioManager : MonoBehaviour
{
    static AudioManager instance;
    public static AudioManager Instance { get { return instance; } }

    [Header("MIX")]
    [SerializeField] AudioMixer audioMixer;

    [Header("BGM")]
    [SerializeField] AudioClip bgmClip;
    public AudioSource bgmPlayer;
    [Header("SFX")]
    [SerializeField] GameObject sfxObject;
    [SerializeField] AudioClip[] sfxClips;
    [SerializeField] int channels;
    public AudioSource[] sfxPlayers;
    [SerializeField] AudioSource fireSFXPlayer;
    int channelIndex;
    bool isFirePlaying;

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
        bgmPlayer.outputAudioMixerGroup = audioMixer.FindMatchingGroups("BGM")[0];
        bgmPlayer.playOnAwake = true;
        bgmPlayer.loop = true;
        bgmPlayer.clip = bgmClip;

        sfxPlayers = new AudioSource[channels];

        for(int i = 0; i < sfxPlayers.Length; i++)
        {
            sfxPlayers[i] = sfxObject.AddComponent<AudioSource>();
            sfxPlayers[i].outputAudioMixerGroup = audioMixer.FindMatchingGroups("SFX")[0];
            sfxPlayers[i].playOnAwake = false;
            sfxPlayers[i].loop = false;
        }
        // Soldier_Fire 전용 AudioSource 초기화
        fireSFXPlayer = sfxObject.AddComponent<AudioSource>();
        fireSFXPlayer.playOnAwake = false;
        fireSFXPlayer.loop = false;
    }
    #region Play Sound
    public void PlayBGM(bool isPlay)
    {
        if (isPlay) bgmPlayer.Play();
        else bgmPlayer.Stop();
    }
    public void PlaySFX(SFX sfx)
    {
        if (sfx == SFX.Soldier_Fire)
        {
            // Soldier_Fire 사운드가 이미 재생 중이라면 추가 재생을 막음
            if (isFirePlaying) return;

            
            //int randomIndex = Random.Range(0, 3);

            // Soldier_Fire 사운드 재생
            //fireSFXPlayer.clip = sfxClips[(int)SFX.Soldier_Fire + randomIndex];
            fireSFXPlayer.clip = sfxClips[(int)SFX.Soldier_Fire];
            fireSFXPlayer.Play();
            isFirePlaying = true;

            // 사운드가 끝나면 다시 재생 가능하게 함
            StartCoroutine(ResetFirePlaying(fireSFXPlayer.clip.length));
        }
        for (int i = 0; i< sfxPlayers.Length;i++)
        {
            int loopIndex = (i + channelIndex) % sfxPlayers.Length;
            if (sfxPlayers[i].isPlaying) continue;

            channelIndex = loopIndex;
            

            sfxPlayers[loopIndex].clip = sfxClips[(int)sfx];

            sfxPlayers[loopIndex].Play();

            break;

        }
    }
    private IEnumerator ResetFirePlaying(float delay)
    {
        yield return new WaitForSeconds(delay);
        isFirePlaying = false;
    }
    #endregion

    #region VolumeController
    public void MasterVolume(float volume)
    {
        audioMixer.SetFloat("MasterVolume", Mathf.Log10(volume) * 20);
    }
    
    public void BGMVolume(float volume)
    {
        audioMixer.SetFloat("BGMVolume", Mathf.Log10(volume) * 20);
    }
    public void SFXVolume(float volume)
    {
        audioMixer.SetFloat("SFXVolume", Mathf.Log10(volume) * 20);
    }

    public float GetMasterVolume()
    {
        float volume;
        if (audioMixer.GetFloat("MasterVolume", out volume))
        {
            volume = Mathf.Pow(10f, volume / 20f);
            return volume;
        }
        else
        {
            Debug.Log("MasterVolume 파라미터가 없음");
            return 0;
        }
    }
    public float GetBGMVolume()
    {
        float volume;
        if (audioMixer.GetFloat("BGMVolume", out volume))
        {
            volume = Mathf.Pow(10f, volume / 20f);
            return volume;
        }
        else
        {
            Debug.Log("BGMVolume 파라미터가 없음");
            return 0;
        }
    }
    public float GetSFXVolume()
    {
        float volume;
        if (audioMixer.GetFloat("SFXVolume", out volume))
        {
            volume = Mathf.Pow(10f, volume / 20f);
            return volume;
        }
        else
        {
            Debug.Log("SFXVolume 파라미터가 없음");
            return 0;
        }
    }
    #endregion
}
