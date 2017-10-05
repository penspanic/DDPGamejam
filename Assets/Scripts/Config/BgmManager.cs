using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum BgmType
{
    Undefined = 0,
    Main,
    Lobby,
    End_Of_Bgm
}

public class BgmManager : Singleton<BgmManager>
{
    public bool Initialized { get; private set; }
    private AudioSource bgmSource;
    private Dictionary<BgmType, AudioClip> bgmClips = new Dictionary<BgmType, AudioClip>();
    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(this.gameObject);

        Initialize();

        bgmSource = gameObject.AddComponent<AudioSource>();
        bgmSource.loop = true;

        SoundManager.Instance.OnBgmVolumeChanged += OnBgmVolumeChanged;
    }

    public void Initialize()
    {
        if(Initialized == true)
        {
            return;
        }

        Initialized = true;

        for(int i = (int)BgmType.Undefined + 1; i < (int)BgmType.End_Of_Bgm; ++i)
        {
            BgmType type = (BgmType)i;
            bgmClips.Add(type, Resources.Load<AudioClip>("Sounds/Bgm/" + type.ToString()));
        }
    }

    private void OnBgmVolumeChanged(float value)
    {
        bgmSource.volume = value;
    }

    public void Play(BgmType type, bool loop = true)
    {
        bgmSource.Stop();
        bgmSource.volume = SoundManager.Instance.BgmVolume;
        bgmSource.clip = bgmClips[type];
        bgmSource.Play();
    }
}