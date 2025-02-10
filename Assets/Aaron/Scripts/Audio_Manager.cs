using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Rendering;

public class Audio_Manager : MonoBehaviour
{
    public static Audio_Manager Instance;

    [Header("Audio Source")]
    public AudioSource bgmSource;
    public AudioSource sfxSource;

    [Header("Audio Clips")]
    public AudioClip bgmClip;
    public List<AudioClip> sfxClip;

    [Header("Sliders")]
    public Slider bgmSlide;
    public Slider sfxSlide;

    [Header("Mute Toggles")]
    public Toggle bgmToggle;
    public Toggle sfxToggle;

    private bool isBGMMuted = false;
    private bool isSFXMuted = false;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        bgmSlide.onValueChanged.AddListener(SetBGMVol);
        sfxSlide.onValueChanged.AddListener(SetSFXVol);

        bgmToggle.onValueChanged.AddListener(ToggleBGM);
        sfxToggle.onValueChanged.AddListener(ToggleSFX);

        sfxSlide.onValueChanged.AddListener(delegate { PlaySFX(0); });

        bgmSlide.value = PlayerPrefs.GetFloat("BGMVolume", 1f);
        sfxSlide.value = PlayerPrefs.GetFloat("SFXVolume", 1f);

        PlayBGM();
    }

    public void SetBGMVol(float volume)
    {
        bgmSource.volume = volume;
        PlayerPrefs.SetFloat("BGMVolume", volume);
    }

    public void SetSFXVol(float volume)
    {
        sfxSource.volume = volume;
        PlayerPrefs.SetFloat("SFXVolume", volume);
    }

    public void PlayBGM()
    {
        bgmSource.clip = bgmClip;
        bgmSource.loop = true;
        bgmSource.Play();
        StartCoroutine(ReplayBGM());
    }

    public void PlaySFX(int index)
    {
        if (index >= 0 && index < sfxClip.Count)
        {
            sfxSource.Stop();
            sfxSource.PlayOneShot(sfxClip[index]);
        }
    }

    public void ToggleBGM(bool isMuted)
    {
        isBGMMuted = isMuted;
        bgmSource.mute = isBGMMuted;
        bgmSlide.interactable = !isBGMMuted;
    }

    public void ToggleSFX(bool isMuted)
    {
        isSFXMuted = isMuted;
        sfxSource.mute = isSFXMuted;
        sfxSlide.interactable = !isSFXMuted;
    }

    private IEnumerator ReplayBGM()
    {
        yield return new WaitForSeconds(bgmClip.length);
        PlayBGM();
    }
}
