using UnityEngine;

public enum SFX { Dead }

public class AudioManager : MonoBehaviour
{
    static AudioManager instance;
    public static AudioManager Instance { get { return instance; } }

    [Header("BGM")]
    [SerializeField] AudioClip bgmClip;
    [SerializeField] float bgmVolume;
    [SerializeField] AudioSource bgmPlayer;
    [Header("SFX")]
    [SerializeField] GameObject sfxObject;
    [SerializeField] AudioClip[] sfxClips;
    [SerializeField] float sfxVolume;
    [SerializeField] int channels;
    [SerializeField] AudioSource[] sfxPlayers;
    int channelIndex;

    

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
    }
    private void Start()
    {
        InitializeObject();
    }

    void InitializeObject()
    {
        bgmPlayer.playOnAwake = true;
        bgmPlayer.loop = true;
        bgmPlayer.volume = bgmVolume;
        bgmPlayer.clip = bgmClip;

        sfxPlayers = new AudioSource[channels];

        for(int i = 0; i < sfxPlayers.Length; i++)
        {
            sfxPlayers[i] = sfxObject.AddComponent<AudioSource>();
            sfxPlayers[i].playOnAwake = false;
            sfxPlayers[i].loop = false;
        }
    }

    public void PlaySFX(SFX sfx)
    {
        for(int i = 0; i< sfxPlayers.Length;i++)
        {
            int loopIndex = (i + channelIndex) % sfxPlayers.Length;
            if (sfxPlayers[i].isPlaying) continue;

            channelIndex = loopIndex;
            sfxPlayers[loopIndex].clip = sfxClips[(int)sfx];
            sfxPlayers[loopIndex].Play();
            break;
        }
    }
}
