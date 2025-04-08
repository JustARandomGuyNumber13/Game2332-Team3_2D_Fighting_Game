using UnityEngine;
using UnityEngine.Audio;

public class ManageAudio : MonoBehaviour
{
    public static ManageAudio Instance;

    [SerializeField] public AudioMixer mixer; //For the audio moxer itself
    [SerializeField] AudioSource bgmSource, sfxSource; //For the gameobject audio sources

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }

        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public void PlayBGM(AudioClip bgm)
    {
        bgmSource.clip = bgm;
        bgmSource.Play();
    }

    public void PlaySFX(AudioClip sfx)
    {
        sfxSource.clip = sfx;
        sfxSource.Play();
    }

    public void SetVolume(string parameter, float volume)
    {
        mixer.SetFloat(parameter, Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat(parameter, volume);
    }

    public void SetMasterVolume(float volume)
    {
        SetVolume("MasterVolume", volume);
    }

    public void SetBGMVolume(float volume)
    {
        SetVolume("BGMVolume", volume);
    }

    public void SetSFXVolume(float volume)
    {
        SetVolume("SFXVolume", volume);
    }

    public void Mute(string parameter)
    {
        SetVolume(parameter, -80f);
    }

    public void UnMute(string parameter, float volume)
    {
        SetVolume(parameter, volume);
    }

    private void LoadVolumeSetting()
    {
        SetMasterVolume(PlayerPrefs.GetFloat("MasterVolume", 0.75f)); //Setting default volume
        SetBGMVolume(PlayerPrefs.GetFloat("BGMVolume", 0.75f));
        SetSFXVolume(PlayerPrefs.GetFloat("SFXVolume", 0.75f));
    }
}