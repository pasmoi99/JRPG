using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("          Audio Source      ")]
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource SFXSource; 

    [Header("         Audio Clip         ")]
    public AudioClip Background;
    public AudioClip Death;
    public AudioClip Attack;
    public AudioClip Hit;

    private void Start()
    {
        musicSource.clip = Background;
        musicSource.Play();
    }

    public void PlaySFX(AudioClip clip)
    {
        SFXSource.PlayOneShot(clip);
    }
}