using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private AudioSource ClickSound;
    private AudioSource WinSound;
    private AudioSource BackGroundMusic;
    private AudioSource SpinSound;

    public AudioSource[] audioSources;

    private static AudioManager inst = null;

    public static AudioManager GetAudioManager()
    {
        return inst;
    }

    public void Awake()
    {
        inst = this;

        audioSources = GetComponentsInChildren<AudioSource>();

        ClickSound = audioSources[0];
        ClickSound.loop = false;

        WinSound = audioSources[1];
        WinSound.loop = false;

        BackGroundMusic = audioSources[2];
        BackGroundMusic.loop = true;

        SpinSound = audioSources[3];
        SpinSound.loop = true;
    }

    public void PlayClickSound()
    {
        ClickSound.Play();
    }
    public void PlayWinSound()
    {
        WinSound.Play();
    }
    public void PlaySpinSound() 
    {
        SpinSound.Play();
    }
    public void StopSpinSound()
    {
        SpinSound.Stop();
    }
}
