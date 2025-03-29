using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.Windows;

[System.Serializable]
public class SkillAudioMapping
{
    public SO_SkillStat skillStat;
    public AudioClip sfxClip;
}

[System.Serializable]
public class ButtonAudioMapping
{
    public string actionMapName;
    public AudioClip buttonClip;
}

public class AudioPlayer : MonoBehaviour
{
    [SerializeField] private AudioSource _bgm, _sfx;
    [SerializeField] private AudioClip _menuBGM, _gameBGM, _selectionBGM;
    [SerializeField] private SkillAudioMapping[] skillAudioMappings;
    [SerializeField] private InputActionReference p1MoveRight, p1MoveLeft, p1Confirm, p1GoBack, p1Ready;
    [SerializeField] private InputActionReference p2MoveRight, p2MoveLeft, p2Confirm, p2GoBack, p2Ready;
    [SerializeField] private AudioClip _p1MoveRClip, _p1MoveLClip, _p1ConfirmClip, _p1GoBackClip, _p1ReadyClip;
    [SerializeField] private AudioClip _p2MoveRClip, _p2MoveLClip, _p2ConfirmClip, _p2GoBackClip, _p2ReadyClip;

    public static AudioPlayer _instance;

    private Dictionary<SO_SkillStat, AudioClip> _sfxMapping = new Dictionary<SO_SkillStat, AudioClip>();
    private Dictionary<string, AudioClip> _buttonMapping = new Dictionary<string, AudioClip>();

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
            return;
        }

        _instance = this;
        DontDestroyOnLoad(gameObject);
        InitializeSFXMapping();
    }

    //Audio Mapping
    private void InitializeSFXMapping()
    {
        foreach (var mapping in skillAudioMappings)
        {
            if (mapping.skillStat != null && mapping.sfxClip != null)
            {
                _sfxMapping.Add(mapping.skillStat, mapping.sfxClip);
            }
            else
            {
                Debug.LogWarning("Skill or SFX are not assigned!");
            }
        }
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;

        //Player 1
        p1MoveRight.action.Enable();
        p1MoveLeft.action.Enable();
        p1Confirm.action.Enable();
        p1GoBack.action.Enable();
        p1Ready.action.Enable();

        p1MoveRight.action.started += _ => Public_PlaySoundEffect(_p1MoveRClip);
        p1MoveLeft.action.started += _ => Public_PlaySoundEffect(_p1MoveLClip);
        p1Confirm.action.started += _ => Public_PlaySoundEffect(_p1ConfirmClip);
        p1GoBack.action.started += _ => Public_PlaySoundEffect(_p1GoBackClip);
        p1Ready.action.started += _ => Public_PlaySoundEffect(_p1ReadyClip);

        //Player 2
        p2MoveRight.action.Enable();
        p2MoveLeft.action.Enable();
        p2Confirm.action.Enable();
        p2GoBack.action.Enable();
        p2Ready.action.Enable();

        p2MoveRight.action.started += _ => Public_PlaySoundEffect(_p2MoveRClip);
        p2MoveLeft.action.started += _ => Public_PlaySoundEffect(_p2MoveLClip);
        p2Confirm.action.started += _ => Public_PlaySoundEffect(_p2ConfirmClip);
        p2GoBack.action.started += _ => Public_PlaySoundEffect(_p2GoBackClip);
        p2Ready.action.started += _ => Public_PlaySoundEffect(_p2ReadyClip);
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;

        //Player 1
        p1MoveRight.action.Disable();
        p1MoveLeft.action.Disable();
        p1Confirm.action.Disable();
        p1GoBack.action.Disable();
        p1Ready.action.Disable();

        //Player 2
        p2MoveRight.action.Disable();
        p2MoveLeft.action.Disable();
        p2Confirm.action.Disable();
        p2GoBack.action.Disable();
        p2Ready.action.Disable();
    }

    private void OnSceneLoaded(Scene _scene, LoadSceneMode _mode)
    {
        switch (_scene.name)
        {
            case "Test 1-MainMenu":
                Public_PlayBackgroundMusic(_menuBGM);
                break;
            case "Main-GamePlayer_Scene"://"Test 2-GamePlay":
                Public_PlayBackgroundMusic(_gameBGM);
                break;
            case "Character-Skill Selection Scene":
                Public_PlayBackgroundMusic(_selectionBGM);
                break;
            default:
                _bgm.Stop();
                break;
        }
    }
    public void Public_PlaySoundEffect(AudioClip audioClip)
    {
        _sfx.clip = audioClip;
        _sfx.Play();
    }

    public void Public_PlaySkillSFX(SO_SkillStat skillStat)
    {
        if (_sfxMapping.TryGetValue(skillStat, out AudioClip sfxClip))
        {
            Public_PlaySoundEffect(sfxClip);
        }
        else
        {
            Debug.LogWarning($"No SFX found for skill: {skillStat.name}");
        }
    }
    public void Public_PlayBackgroundMusic(AudioClip audioClip)
    {
        _bgm.clip = audioClip;
        _bgm.Play();
    }

    /*public void OnButtonPress(string inputActionName)
    {
        if (_buttonMapping.TryGetValue(inputActionName, out AudioClip iaClip))
        {
            Public_PlaySoundEffect(iaClip);
        }
        else
        {
            Debug.LogWarning($"No audio clip mapped for Input action: {inputActionName}");
        }
    }*/
}
