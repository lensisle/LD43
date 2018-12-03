using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour, IGameManagerDependency
{
    [SerializeField]
    private AudioClip _defaultClip;
    public AudioClip DefaultClip { get { return _defaultClip; } }

    [SerializeField]
    private AudioSource _musicSource;

    [SerializeField]
    private AudioSource _soundsSource;

    public void Initialize()
    {

    }

    public void PlaySound(AudioClip clip)
    {
        if (_soundsSource.isPlaying)
        {
            _soundsSource.Stop();
        }

        _soundsSource.PlayOneShot(clip);
    }

    public void PlayMusic(AudioClip clip, bool loop = false)
    {
        if (_musicSource.isPlaying)
        {
            _musicSource.Stop();
        }

        SetMusicClip(clip);
        _musicSource.Play();
        _musicSource.loop = loop;
    }

    public void StopMusic()
    {
        _musicSource.Stop();
    }

    public void SetMusicClip(AudioClip clip)
    {
        _musicSource.clip = clip;
    }
	
    public void SetMusicVolume(float vol)
    {
        _musicSource.volume = vol;
    }
}
