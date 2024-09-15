using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioClip SnakeMoveAudio;
    public AudioClip SnakeEatAudio;
    public AudioClip SnakeDieAudio;
    public AudioClip ButtonClickAudio;

    public enum Sound
    {
        SnakeMove,
        SnakeDie,
        SnakeEat,
        ButtonClick,
    }

    public void PlaySound(Sound sound)
    {
        AudioSource audioSource = this.GetComponent<AudioSource>();
        audioSource.PlayOneShot(GetAudioClip(sound));
    }

    private AudioClip GetAudioClip(Sound sound)
    {
        switch (sound) {
            case Sound.SnakeMove:
                return SnakeMoveAudio;
            case Sound.SnakeEat:
                return SnakeEatAudio;
            case Sound.SnakeDie:
                return SnakeDieAudio;
            case Sound.ButtonClick:
                return ButtonClickAudio;
        }
        return null;
    }

}
