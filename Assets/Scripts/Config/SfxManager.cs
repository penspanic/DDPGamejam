using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public enum SfxType
{
    Undefined = 0,
    UI_Click1,
    UI_Click2,
    UI_Click3,
    UI_Alert,
    UI_DragStart,
    UI_DragEnd,
    UI_Result_Normal,
    UI_result_Success,
    Star,
    End_Of_Sfx,
}

public class SfxManager : Singleton<SfxManager>
{
    public bool IsInitialized { get; private set; }

    private Dictionary<SfxType, AudioClip> clips = new Dictionary<SfxType, AudioClip>();
    private AudioSource source = null;
    private ObjectPool<AudioSource> loopSourcesPool = new ObjectPool<AudioSource>();
    private List<KeyValuePair<SfxType, AudioSource>> loopingSources = new List<KeyValuePair<SfxType, AudioSource>>();
    private GameObject loopSourcesObject = null;
    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(this.gameObject);

        Initialize();

        source = gameObject.AddComponent<AudioSource>();
        source.volume = SoundManager.Instance.SfxVolume;

        loopSourcesObject = new GameObject("Loop Sources");
        loopSourcesObject.transform.SetParent(this.transform);
        for (int i = 0; i < 10; ++i)
        {
            AddLoopSource();
        }

        SoundManager.Instance.OnSfxVolumeChanged += OnSfxVolumeChanged;
    }

    private void AddLoopSource()
    {
        AudioSource newSource = loopSourcesObject.AddComponent<AudioSource>();
        newSource.volume = SoundManager.Instance.SfxVolume;
        loopSourcesPool.Add(newSource);
    }

    public void Initialize()
    {
        if (IsInitialized == true)
        {
            return;
        }
        IsInitialized = true;

        for(int i = (int)SfxType.Undefined + 1; i < (int)SfxType.End_Of_Sfx; ++i)
        {
            SfxType type = (SfxType)i;
            clips.Add(type, Resources.Load<AudioClip>("Sounds/Sfx/" + type.ToString()));
        }

        Debug.Log("SfxManager Load end.");
    }

    private void OnSfxVolumeChanged(float value)
    {
        source.volume = value;
        for (int i = 0; i < loopingSources.Count; ++i)
        {
            loopingSources[i].Value.volume = value;
        }
    }

    public void Play(SfxType type)
    {
        if (clips.ContainsKey(type) == false)
        {
            Debug.LogError("Sfx not loaded, type : " + type.ToString());
            return;
        }

        source.PlayOneShot(clips[type], source.volume);
    }

    public void PlayLoop(SfxType type)
    {
        if (clips.ContainsKey(type) == false)
        {
            Debug.LogError("Sfx not loaded, type : " + type.ToString());
            return;
        }
        if (loopSourcesPool.RemainCount == 0)
        {
            AddLoopSource();
        }

        AudioSource loopSource = loopSourcesPool.Get();
        loopSource.volume = SoundManager.Instance.SfxVolume;
        loopSource.clip = clips[type];
        loopSource.loop = true;
        loopSource.Play();

        loopingSources.Add(new KeyValuePair<SfxType, AudioSource>(type, loopSource));
    }

    public void StopLoop(SfxType type)
    {
        int findIndex = int.MinValue;
        for (int i = 0; i < loopingSources.Count; ++i)
        {
            if (loopingSources[i].Key == type)
            {
                findIndex = i;
                break;
            }
        }

        if (findIndex != int.MinValue)
        {
            AudioSource endedSource = loopingSources[findIndex].Value;
            endedSource.Stop();
            endedSource.clip = null;

            loopingSources.RemoveAt(findIndex);

            loopSourcesPool.Add(endedSource);
        }
    }

    public void StopAll()
    {
        for (int i = 0; i < loopingSources.Count; ++i)
        {
            loopingSources[i].Value.Stop();
            loopingSources[i].Value.clip = null;
            loopSourcesPool.Add(loopingSources[i].Value);
        }
        loopingSources.Clear();
    }
}