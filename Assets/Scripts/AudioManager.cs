using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField]
    [Range(0, 1)]
    private float _volume;

    [SerializeField]
    private AudioSource _audioSource;

    public void Play()
    {
        _audioSource.Play();
    }

    public void Stop()
    {
        _audioSource.Stop();
    }

    public void SetClip(AudioClip clip)
    {
        _audioSource.clip = clip;
    }
	
    public void SetVolume(float vol)
    {
        _volume = vol;
        _audioSource.volume = _volume;
    }
}
