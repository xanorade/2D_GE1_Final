using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    // Audio sources for music and sound effects
    [Header("Audio Source")]
    [SerializeField]
    AudioSource musicSource;
    [SerializeField]
    AudioSource SFXSource;

    // Audio clips for various sounds
    [Header("Audio Clip")]
    public AudioClip background; // Background music
    public AudioClip death; // sfx played when the player dies
    public AudioClip expTaken; // sfx played when the player collects experience
    public AudioClip projectile; // sfx played when a projectile is fired
    public AudioClip damageTaken; // sfx played when the player takes damage
    public AudioClip damageGiven; // sfx played when enemy takes damage

    public void Start()
    {
        // Set up the background music
        musicSource.clip = background;
        musicSource.loop = true;
        musicSource.Play();
    }

    // Method to play a sound effect
    public void PlaySFX(AudioClip clip)
    {
        // Play the given sound effect
        SFXSource.PlayOneShot(clip);
    }
}
