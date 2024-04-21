using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using static GameEnums;

[System.Serializable]
public class Sounds
{
    [SerializeField] ESoundName _soundName;

    //Nhận vào audioClip, đỡ phải tạo AS
    [SerializeField] AudioClip _soundAudioClip;

    public ESoundName SoundName { get { return _soundName; } }

    public AudioClip SoundAudioClip { get { return _soundAudioClip; } }
}

public class SoundsManager : BaseSingleton<SoundsManager>
{
    //Clip - tệp âm thanh
    //Source - cái để phát tệp đó cũng như điều chỉnh linh tinh

    [SerializeField]
    Sounds[] _sfxSounds, _musicSounds;

    [SerializeField] AudioSource _sfxSource, _musicSource;
    [SerializeField] float _bgmusicDelay;

    bool _isPlayingBossTheme; //BossTheme sẽ có ngoại lệ != các Theme là nó sẽ tắt khi Replay

    public bool IsPlayingBossTheme { get => _isPlayingBossTheme; }

    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        PlayBackGroundMusic();
    }

    private void PlayBackGroundMusic()
    {
        //PlayMusic(ESoundName.StartMenuTheme);
    }

    public void PlaySfx(ESoundName sfxName, float volumeScale)
    {
        Sounds s = Array.Find(_sfxSounds, x => x.SoundName == sfxName);
        if (s == null)
            Debug.Log(sfxName + " Not Found");
        else
        {
            _sfxSource.clip = s.SoundAudioClip;
            if (volumeScale >= 1.0f) _sfxSource.PlayOneShot(_sfxSource.clip);
            else _sfxSource.PlayOneShot(_sfxSource.clip, volumeScale);
            //Debug.Log("Sfx Played: " + sfxName);
        }
    }

    public void PlayMusic(ESoundName musicName)
    {
        Sounds s = Array.Find(_musicSounds, x => x.SoundName == musicName);
        if (s == null)
            Debug.Log(musicName + " Not Found");
        else
        {
            /*if (musicName == ESoundName.BossTheme)
                _isPlayingBossTheme = true;
            else
                _isPlayingBossTheme = false;*/
            _musicSource.clip = s.SoundAudioClip;
            _musicSource.PlayDelayed(_bgmusicDelay);
        }
    }

    public void ChangeMusicVolume(float para)
    {
        _musicSource.volume = para;
    }

    public void ChangeSfxVolume(float para)
    {
        _sfxSource.volume = para;
    }
}