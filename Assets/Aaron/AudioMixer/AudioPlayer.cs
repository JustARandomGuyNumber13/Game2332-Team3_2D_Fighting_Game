using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.Windows;

//Note: I am aware of the mess of this code and will try to clean it up when everything is completed
//Note: Move skills and selection IA audio to their respective player SFX method if possible
//Note: Check each comment and figure out if it needs to be deleted


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
    [SerializeField] private GameObject p1PrefabInst;
    [SerializeField] private GameObject p2PrefabInst;

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
    private string playerName;

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
                if (!_sfxMapping.ContainsKey(mapping.skillStat))
                {
                    _sfxMapping.Add(mapping.skillStat, mapping.sfxClip);
                    //Debug.Log($"Mapped {mapping.skillStat.name} to {mapping.sfxClip.name}");
                }
                /*else
                {
                    Debug.LogWarning($"Duplicate mapping for {mapping.skillStat.name}. Skipping.");
                }*/
            }
            else
            {
                Debug.LogWarning("Skill or SFX are not assigned!");
            }
        }
    }

    public void RegisteredPlayers(GameObject player1, GameObject player2)
    {
        p1PrefabInst = player1;
        p2PrefabInst = player2;
        Debug.Log($"Players registered: Player 1 = {player1.name}, Player 2 = {player2.name}");
    }

    
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
    public void Public_PlaySkillSFX(SO_SkillStat skillStat, GameObject playerGameObject)
    {
        if (skillStat == null)
        {
            Debug.LogWarning("SkillStat is null. Cannot play SFX.");
            return;
        }

        if (playerGameObject == null)
        {
            Debug.LogWarning("Player GameObject is null. Cannot determine which player triggered the skill.");
            return;
        }

        Debug.Log($"Skill triggered by: {playerGameObject.name}");

        // Match the playerGameObject to player instances
        if (playerGameObject == p1PrefabInst)
        {
            Debug.Log($"Player 1 triggered skill {skillStat.name}, playing audio...");
            Public_PlayP1SoundEffect(_sfxMapping[skillStat]);
        }
        else if (playerGameObject == p2PrefabInst)
        {
            Debug.Log($"Player 2 triggered skill {skillStat.name}, playing audio...");
            Public_PlayP2SoundEffect(_sfxMapping[skillStat]);
        }
        else
        {
            Debug.LogWarning($"Player GameObject does not match registered players for skill: {skillStat.name}. Playing default audio.");
            Public_PlaySoundEffect(_sfxMapping[skillStat]); // Fallback
        }
    }
    public void Public_PlayBackgroundMusic(AudioClip audioClip)
    {
        _bgm.clip = audioClip;
        _bgm.Play();
    }
}
