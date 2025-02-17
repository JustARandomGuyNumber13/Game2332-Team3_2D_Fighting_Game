using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Rendering;

public class Audio_Manager : MonoBehaviour
{
    public static Audio_Manager Instance;
    public BGMNameDisplay songName;

    [Header("Audio Source")]
    public AudioSource bgmSource;
    public AudioSource sfxSource;

    [Header("Audio Clips")]
    public List<AudioClip> bgmClip;
    public List<AudioClip> sfxClip;

    [Header("Sliders")]
    public Slider bgmSlide;
    public Slider sfxSlide;

    [Header("Mute Toggles")]
    public Toggle bgmToggle;
    public Toggle sfxToggle;

    private bool isBGMMuted = false;
    private bool isSFXMuted = false;
    private int currentBGMIndex = 0;
    private bool sfxPlaysAtStart = true;

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
        sfxSlide.onValueChanged.AddListener(OnSFXVolChange);

        bgmToggle.onValueChanged.AddListener(ToggleBGM);
        sfxToggle.onValueChanged.AddListener(ToggleSFX);

        bgmSlide.value = PlayerPrefs.GetFloat("BGMVolume", 1f);
        sfxSlide.value = PlayerPrefs.GetFloat("SFXVolume", 1f);

        sfxPlaysAtStart = false;

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
        if (bgmClip.Count == 0)
        {
            return;
        }

        bgmSource.clip = bgmClip[currentBGMIndex];
        bgmSource.loop = false;
        bgmSource.Play();
        
        songName.DisplaySongName(bgmSource.clip.name);

        currentBGMIndex = (currentBGMIndex + 1) % bgmClip.Count;

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

    public void OnSFXVolChange(float volume)
    {
        SetSFXVol(volume);
        if (!sfxPlaysAtStart)
        {
            PlaySFX(0);
        }
    }

    public void ShowAudioSettings()
    {
        bgmSlide.gameObject.SetActive(true);
        sfxSlide.gameObject.SetActive(true);
        bgmToggle.gameObject.SetActive(true);
        sfxToggle.gameObject.SetActive(true);
    }

    public void ShowBGMname()
    {
        if (songName != null && bgmSource.clip != null)
        {
            songName.DisplaySongName(bgmSource.clip.name);
        }
    }

    private IEnumerator ReplayBGM()
    {
        yield return new WaitForSeconds(bgmSource.clip.length);
        PlayBGM();
    }
}
