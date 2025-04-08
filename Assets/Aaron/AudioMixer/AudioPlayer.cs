using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.Windows;

//Note: I am aware of the mess of this code and will try to clean it up when everything is completed
//Note: Move skills and selection IA audio to their respective player SFX method if possible


[System.Serializable]
public class SkillAudioMapping
{
    public SO_SkillStat skillStat;
    public AudioClip sfxClip;
}

public class AudioPlayer : MonoBehaviour
{
    [SerializeField] private AudioSource _bgm, _sfx, _p1SFX, _p2SFX;
    [SerializeField] private AudioClip _menuBGM, _gameBGM, _selectionBGM;
    [SerializeField] private SkillAudioMapping[] skillAudioMappings;
    private SO_SkillStat[] p1EquippedSkills = new SO_SkillStat[3];
    private SO_SkillStat[] p2EquippedSkills = new SO_SkillStat[3];

    //Player Skill Input Fields
    [Header("Player 1 Skill Inputs")]
    [SerializeField] private InputActionReference p1Skill1;
    [SerializeField] private InputActionReference p1Skill2;
    [SerializeField] private InputActionReference p1Skill3;

    [Header("Player 2 Skill Inputs")]
    [SerializeField] private InputActionReference p2Skill1;
    [SerializeField] private InputActionReference p2Skill2;
    [SerializeField] private InputActionReference p2Skill3;

    //Selection Scene input fields
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

    private void BindSkillInput()
    {
        EnableIA();

        //Player 1
        p1Skill1.action.performed += _ => PlaySkillAudio("Player 1", 0);
        p1Skill2.action.performed += _ => PlaySkillAudio("Player 1", 1);
        p1Skill3.action.performed += _ => PlaySkillAudio("Player 1", 2);

        //Player 2
        p2Skill1.action.performed += _ => PlaySkillAudio("Player 2", 0);
        p2Skill2.action.performed += _ => PlaySkillAudio("Player 2", 1);
        p2Skill3.action.performed += _ => PlaySkillAudio("Player 2", 2);
    }

    private void UnbindSkillInput()
    {
        DisableIA();

        //Player 1
        p1Skill1.action.performed -= _ => PlaySkillAudio("Player 1", 0);
        p1Skill2.action.performed -= _ => PlaySkillAudio("Player 1", 1);
        p1Skill3.action.performed -= _ => PlaySkillAudio("Player 1", 2);

        //Player 2
        p2Skill1.action.performed -= _ => PlaySkillAudio("Player 2", 0);
        p2Skill2.action.performed -= _ => PlaySkillAudio("Player 2", 1);
        p2Skill3.action.performed -= _ => PlaySkillAudio("Player 2", 2);
    }

    private void PlaySkillAudio(string playerName, int skillIndex)
    {
        SO_SkillStat skill = null;

        if (playerName == "Player 1")
        {
            skill = p1EquippedSkills[skillIndex];
        }
        else if (playerName == "Player 2")
        {
            skill = p2EquippedSkills[skillIndex];
        }

        if (skill != null && _sfxMapping.TryGetValue(skill, out AudioClip skillClip))
        {
            Debug.Log($"{playerName} triggered skill {skill.name} with audio {skillClip.name}");

            if (playerName == "Player 1")
            {
                Public_PlayP1SoundEffect(skillClip);
            }
            else if (playerName == "Player 2")
            {
                Public_PlayP2SoundEffect(skillClip);
            }
            else
            {
                Debug.LogWarning($"No SFX found for {playerName} skill at index {skillIndex}");
            }
        }
    }

    public void AssignSkillIndex(string playername, int skillIndex, SO_SkillStat skill)
    {
        if (playername == "Player 1")
        {
            p1EquippedSkills[skillIndex] = skill;
        }
        else if (playername == "Player 2")
        {
            p2EquippedSkills[skillIndex] = skill;
        }
        Debug.Log($"{playername} equipped {skill.name} in slot {skillIndex}");
    }
    /*private SO_SkillStat GetSkillStat(string playerName, int skillIndex)
    {
        if (playerName == "Player 1")
        {
            return skillAudioMappings[skillIndex].skillStat;
        }

        if (playerName == "Player 2")
        {
            return skillAudioMappings[skillIndex].skillStat;
        }
        return null;
    }*/
    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    //For Player selection scene
    //When player selection scene is active
    private void EnableAudioMapping()
    {
        EnableIA();

        //Player 1
        p1MoveRight.action.started += _ => Public_PlayP1SoundEffect(_p1MoveRClip);
        p1MoveLeft.action.started += _ => Public_PlayP1SoundEffect(_p1MoveLClip);
        p1Confirm.action.started += _ => Public_PlayP1SoundEffect(_p1ConfirmClip);
        p1GoBack.action.started += _ => Public_PlayP1SoundEffect(_p1GoBackClip);
        p1Ready.action.started += _ => Public_PlayP1SoundEffect(_p1ReadyClip);

        //Player 2
        p2MoveRight.action.started += _ => Public_PlayP2SoundEffect(_p2MoveRClip);
        p2MoveLeft.action.started += _ => Public_PlayP2SoundEffect(_p2MoveLClip);
        p2Confirm.action.started += _ => Public_PlayP2SoundEffect(_p2ConfirmClip);
        p2GoBack.action.started += _ => Public_PlayP2SoundEffect(_p2GoBackClip);
        p2Ready.action.started += _ => Public_PlayP2SoundEffect(_p2ReadyClip);
    }

    //When Player selection scene is not active
    private void DisableAudioMapping()
    {
        DisableIA();

        //Player 1
        p1MoveRight.action.started -= _ => Public_PlayP1SoundEffect(_p1MoveRClip);
        p1MoveLeft.action.started -= _ => Public_PlayP1SoundEffect(_p1MoveLClip);
        p1Confirm.action.started -= _ => Public_PlayP1SoundEffect(_p1ConfirmClip);
        p1GoBack.action.started -= _ => Public_PlayP1SoundEffect(_p1GoBackClip);
        p1Ready.action.started -= _ => Public_PlayP1SoundEffect(_p1ReadyClip);

        //Player 2
        p2MoveRight.action.started -= _ => Public_PlayP2SoundEffect(_p2MoveRClip);
        p2MoveLeft.action.started -= _ => Public_PlayP2SoundEffect(_p2MoveLClip);
        p2Confirm.action.started -= _ => Public_PlayP2SoundEffect(_p2ConfirmClip);
        p2GoBack.action.started -= _ => Public_PlayP2SoundEffect(_p2GoBackClip);
        p2Ready.action.started -= _ => Public_PlayP2SoundEffect(_p2ReadyClip);
    }

    private void EnableIA()
    {
        //Player 1
        p1MoveRight.action.Enable();
        p1MoveLeft.action.Enable();
        p1Confirm.action.Enable();
        p1GoBack.action.Enable();
        p1Ready.action.Enable();

        p1Skill1.action.Enable();
        p1Skill2.action.Enable();
        p1Skill3.action.Enable();

        //Player 2
        p2MoveRight.action.Enable();
        p2MoveLeft.action.Enable();
        p2Confirm.action.Enable();
        p2GoBack.action.Enable();
        p2Ready.action.Enable();

        p2Skill1.action.Enable();
        p2Skill2.action.Enable();
        p2Skill3.action.Enable();

    }

    private void DisableIA()
    {
        //Player 1
        p1MoveRight.action.Disable();
        p1MoveLeft.action.Disable();
        p1Confirm.action.Disable();
        p1GoBack.action.Disable();
        p1Ready.action.Disable();

        p1Skill1.action.Disable();
        p1Skill2.action.Disable();
        p1Skill3.action.Disable();

        //Player 2
        p2MoveRight.action.Disable();
        p2MoveLeft.action.Disable();
        p2Confirm.action.Disable();
        p2GoBack.action.Disable();
        p2Ready.action.Disable();

        p2Skill1.action.Disable();
        p2Skill2.action.Disable();
        p2Skill3.action.Disable();
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

        if ( _scene.name == "Main-GamePlayer_Scene")
        {
            BindSkillInput();
        }

        else
        {
            UnbindSkillInput();
        }
    }

    //Play sounds
    public void Public_PlaySoundEffect(AudioClip audioClip)
    {
        _sfx.clip = audioClip;
        _sfx.Play();
    }

    //Player 1
    public void Public_PlayP1SoundEffect(AudioClip audioClip)
    {
        _p1SFX.clip = audioClip;
        _p1SFX.Play();
    }

    //Player 2
    public void Public_PlayP2SoundEffect(AudioClip audioClip)
    {
        _p2SFX.clip = audioClip;
        _p2SFX.Play();
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
