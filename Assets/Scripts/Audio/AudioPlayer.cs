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

public class AudioPlayer : MonoBehaviour
{
    [SerializeField] private AudioSource _bgm, _sfx;
    [SerializeField] private AudioClip _menuBGM, _gameBGM, _selectionBGM;
    [SerializeField] private SkillAudioMapping[] skillAudioMappings;

    [Header("Player 1 Input Actions")]
    [SerializeField] private InputActionReference p1MoveRight;
    [SerializeField] private InputActionReference p1MoveLeft;
    [SerializeField] private InputActionReference p1Confirm;
    [SerializeField] private InputActionReference p1GoBack;
    [SerializeField] private InputActionReference p1Ready;

    [Header("Player 2 Input Actions")]
    [SerializeField] private InputActionReference p2MoveRight;
    [SerializeField] private InputActionReference p2MoveLeft;
    [SerializeField] private InputActionReference p2Confirm;
    [SerializeField] private InputActionReference p2GoBack;
    [SerializeField] private InputActionReference p2Ready;

    [Header("Player 1 IA Clips")]
    [SerializeField] private AudioClip _p1MoveRClip;
    [SerializeField] private AudioClip _p1MoveLClip;
    [SerializeField] private AudioClip _p1ConfirmClip;
    [SerializeField] private AudioClip _p1GoBackClip;
    [SerializeField] private AudioClip _p1ReadyClip;

    [Header("Player 2 IA Clips")]
    [SerializeField] private AudioClip _p2MoveRClip;
    [SerializeField] private AudioClip _p2MoveLClip;
    [SerializeField] private AudioClip _p2ConfirmClip;
    [SerializeField] private AudioClip _p2GoBackClip;
    [SerializeField] private AudioClip _p2ReadyClip;

    public static AudioPlayer _instance;

    private Dictionary<SO_SkillStat, AudioClip> _sfxMapping = new Dictionary<SO_SkillStat, AudioClip>();

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
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void EnableAudioMapping()
    {
        EnableIA();

        //Player 1
        p1MoveRight.action.started += _ => Public_PlaySoundEffect(_p1MoveRClip);
        p1MoveLeft.action.started += _ => Public_PlaySoundEffect(_p1MoveLClip);
        p1Confirm.action.started += _ => Public_PlaySoundEffect(_p1ConfirmClip);
        p1GoBack.action.started += _ => Public_PlaySoundEffect(_p1GoBackClip);
        p1Ready.action.started += _ => Public_PlaySoundEffect(_p1ReadyClip);

        //Player 2
        p2MoveRight.action.started += _ => Public_PlaySoundEffect(_p2MoveRClip);
        p2MoveLeft.action.started += _ => Public_PlaySoundEffect(_p2MoveLClip);
        p2Confirm.action.started += _ => Public_PlaySoundEffect(_p2ConfirmClip);
        p2GoBack.action.started += _ => Public_PlaySoundEffect(_p2GoBackClip);
        p2Ready.action.started += _ => Public_PlaySoundEffect(_p2ReadyClip);
    }

    private void DisableAudioMapping()
    {
        DisableIA();

        //Player 1
        p1MoveRight.action.started -= _ => Public_PlaySoundEffect(_p1MoveRClip);
        p1MoveLeft.action.started -= _ => Public_PlaySoundEffect(_p1MoveLClip);
        p1Confirm.action.started -= _ => Public_PlaySoundEffect(_p1ConfirmClip);
        p1GoBack.action.started -= _ => Public_PlaySoundEffect(_p1GoBackClip);
        p1Ready.action.started -= _ => Public_PlaySoundEffect(_p1ReadyClip);

        //Player 2
        p2MoveRight.action.started -= _ => Public_PlaySoundEffect(_p2MoveRClip);
        p2MoveLeft.action.started -= _ => Public_PlaySoundEffect(_p2MoveLClip);
        p2Confirm.action.started -= _ => Public_PlaySoundEffect(_p2ConfirmClip);
        p2GoBack.action.started -= _ => Public_PlaySoundEffect(_p2GoBackClip);
        p2Ready.action.started -= _ => Public_PlaySoundEffect(_p2ReadyClip);
    }

    private void EnableIA()
    {
        //Player 1
        p1MoveRight.action.Enable();
        p1MoveLeft.action.Enable();
        p1Confirm.action.Enable();
        p1GoBack.action.Enable();
        p1Ready.action.Enable();


        //Player 2
        p2MoveRight.action.Enable();
        p2MoveLeft.action.Enable();
        p2Confirm.action.Enable();
        p2GoBack.action.Enable();
        p2Ready.action.Enable();
    }

    private void DisableIA()
    {
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

        //Ensuring that audio clips assigned only play if the scene is the player selection scene
        if (_scene.name == "Character-Skill Selection Scene")
        {
            EnableAudioMapping();
        }

        else
        {
            DisableAudioMapping();
        }
    }

    //Play sounds
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
}
