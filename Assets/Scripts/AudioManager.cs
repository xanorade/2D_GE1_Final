using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("Audio Source")]
    [SerializeField]
    AudioSource musicSource;
    [SerializeField]
    AudioSource SFXSource;

    [Header("Audio Clip")]
    public AudioClip background;
    public AudioClip death;
    public AudioClip expTaken;
    public AudioClip projectile;
    public AudioClip damageTaken;

    public void PlaySFX(AudioClip clip)
    { 
    SFXSource.PlayOneShot(clip);
    }
}
