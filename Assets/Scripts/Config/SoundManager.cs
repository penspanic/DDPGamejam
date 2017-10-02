using UnityEngine;
using System.Collections;

public enum SoundConfigType
{
    MasterVolume,
    BgmVolume,
    SfxVolume,
}

public class SoundManager : Singleton<SoundManager>
{
    public float MasterVolume
    {
        get
        {
            return _masterVolume;
        }
        set
        {
            _masterVolume = value;

            if(OnMasterVolumeChanged != null)
            {
                OnMasterVolumeChanged(_masterVolume);
            }
            if(OnBgmVolumeChanged != null)
            {
                OnBgmVolumeChanged(_masterVolume * _bgmVolume);
            }
            if(OnSfxVolumeChanged != null)
            {
                OnSfxVolumeChanged(_masterVolume * _sfxVolume);
            }
        }
    }
    private float _masterVolume;

    public float BgmVolume
    {
        get
        {
            return _bgmVolume;
        }
        set
        {
            _bgmVolume = value;
            if(OnBgmVolumeChanged != null)
            {
                OnBgmVolumeChanged(_masterVolume * _bgmVolume);
            }
        }
    }
    private float _bgmVolume;

    public float SfxVolume
    {
        get
        {
            return _sfxVolume;
        }
        set
        {
            _sfxVolume = value;
            if(OnSfxVolumeChanged != null)
            {
                OnSfxVolumeChanged(_masterVolume * _sfxVolume);
            }
        }
    }
    private float _sfxVolume;

    #region Event
    public event System.Action<float> OnMasterVolumeChanged;
    public event System.Action<float> OnBgmVolumeChanged;
    public event System.Action<float> OnSfxVolumeChanged;
    #endregion

    protected override void Awake()
    {
        base.Awake();

        MasterVolume = 1f;
        BgmVolume = 1f;
        SfxVolume = 1f;

        if (PlayerPrefs.HasKey(SoundConfigType.MasterVolume.ToString()) == true)
        {
            MasterVolume = PlayerPrefs.GetFloat(SoundConfigType.MasterVolume.ToString());
        }
        if (PlayerPrefs.HasKey(SoundConfigType.BgmVolume.ToString()) == true)
        {
            BgmVolume = PlayerPrefs.GetFloat(SoundConfigType.BgmVolume.ToString());
        }
        if (PlayerPrefs.HasKey(SoundConfigType.SfxVolume.ToString()) == true)
        {
            SfxVolume = PlayerPrefs.GetFloat(SoundConfigType.SfxVolume.ToString());
        }
    }

    private void OnApplicationQuit()
    {
        PlayerPrefs.SetFloat(SoundConfigType.MasterVolume.ToString(), MasterVolume);
        PlayerPrefs.SetFloat(SoundConfigType.BgmVolume.ToString(), BgmVolume);
        PlayerPrefs.SetFloat(SoundConfigType.SfxVolume.ToString(), SfxVolume);
        PlayerPrefs.Save();
    }
}