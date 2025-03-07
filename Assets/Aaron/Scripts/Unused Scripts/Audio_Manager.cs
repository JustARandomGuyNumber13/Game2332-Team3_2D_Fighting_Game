using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Rendering;
using UnityEngine.Events;

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

    [Header("Events")]
    public UnityEvent<int> OnPlaySFX;

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
        //InitializeAudio();
        PlayBGM();
    }

    private void InitializeAudio()
    {
        //Null checks to ensure that erros do not happen in case the volume sliders are not present
        //if (bgmSlide != null)
        //{
            Debug.Log("BGM slider assigned");
            bgmSlide.onValueChanged.AddListener(SetBGMVol);
            bgmSlide.value = PlayerPrefs.GetFloat("BGMVolume", 1f);
        //}

        if (sfxSlide != null)
        {
            Debug.Log("SFX slider assigned");
            sfxSlide.onValueChanged.AddListener(OnSFXVolChange);
            sfxSlide.value = PlayerPrefs.GetFloat("SFXVolume", 1f);
        }

        if (bgmToggle != null)
        {
            Debug.Log("BGM toggle assigned");
            bgmToggle.onValueChanged.AddListener(ToggleBGM);
        }

        if (sfxToggle != null)
        {
            Debug.Log("SFX toggle assigned");
            sfxToggle.onValueChanged.AddListener(ToggleSFX);
        }

        if (OnPlaySFX == null) //Quick reference in how it we might go about this if this won't change, scroll to bottom of script
        {
            OnPlaySFX = new UnityEvent<int>();
        }

        sfxPlaysAtStart = false;
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
        
        if (songName != null)
        {
            songName.DisplaySongName(bgmSource.clip.name);
        }

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
        if (bgmSlide != null)
        {
            bgmSlide.interactable = !isBGMMuted;
        }
    }

    public void ToggleSFX(bool isMuted)
    {
        isSFXMuted = isMuted;
        sfxSource.mute = isSFXMuted;
        if (sfxSlide != null)
        {
            sfxSlide.interactable = !isSFXMuted;
        }
    }

    public void OnSFXVolChange(float volume)
    {
        SetSFXVol(volume);
        if (!sfxPlaysAtStart)
        {
            PlaySFX(0);
        }
    }

    /*public void ShowAudioSettings()
    {
        bgmSlide.gameObject?.SetActive(true);
        sfxSlide.gameObject?.SetActive(true);
        bgmToggle.gameObject?.SetActive(true);
        sfxToggle.gameObject?.SetActive(true);
    }*/

    public void UpdateUIComponents(Slider newBgmSlider, Slider newSfxSlider, Toggle newBgmToggle, Toggle newSfxToggle)
    {
        bgmSlide = newBgmSlider;
        sfxSlide = newSfxSlider;
        bgmToggle = newBgmToggle;
        sfxToggle = newSfxToggle;
        Debug.Log("UI components updated")
;       InitializeAudio();
    }
    public void AssignUIComponents(GameObject parentObject)
    {
        Slider newBgmSlider = parentObject.transform.Find("BGMSlider").GetComponent<Slider>();
        Slider newSfxSlider = parentObject.transform.Find("SFXSlider").GetComponent<Slider>();
        Toggle newBgmToggle = parentObject.transform.Find("BGMMute").GetComponent<Toggle>();
        Toggle newSfxToggle = parentObject.transform.Find("SFXMute").GetComponent<Toggle>();

        if (newBgmSlider == null || newSfxSlider == null || newBgmToggle == null || newSfxToggle == null)
        {
            Debug.LogError("One or more UI components could not be found");
        }
        else
        {
            UpdateUIComponents(newBgmSlider, newSfxSlider, newBgmToggle, newSfxToggle);
        }     
    }

    public void ShowBGMname()
    {
        if (songName != null && bgmSource.clip != null)
        {
            songName.DisplaySongName(bgmSource.clip.name);
        }

        else
        {
            Debug.Log("Display Name is not assigned yet!");
        }
    }

    private IEnumerator ReplayBGM()
    {
        yield return new WaitForSeconds(bgmSource.clip.length);
        PlayBGM();
    }


    /*
     * Possibly won't work for final product but might work within the confines of getting to milestone 2
     * Ex: within Ninja_Skill_ShootProjectile_ConfusingBomb
     * add [Serializefield] private int sfxIndex; //To specify which sfx to use
     * 
     * within TriggerSkill()
     * add if (Audio_manager.Instance != null)
     * {
     *     Audio.Manager.Instance.OnPlaySFX.Invoke(sfxIndex)
     * }
     */
}
