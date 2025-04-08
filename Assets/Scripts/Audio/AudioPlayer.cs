<<<<<<< HEAD
using UnityEngine;
using UnityEngine.Audio;

public class AudioPlayer : MonoBehaviour
{
    [SerializeField] private AudioSource _bgm, _sfx;

=======
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.Windows;

//Note: I am aware of the mess of this code and will try to clean it up when everything is completed
//Note: Rework or Delete Defense and Jump Audio code. Current method is not working at the moment as player 2 actions somehow influence player 1 actions
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

    //Selection Scene input fields
    [Header("Player 1 Input Actions")]
    [SerializeField] private InputActionReference p1MoveRight;
    [SerializeField] private InputActionReference p1MoveLeft;
    [SerializeField] private InputActionReference p1Confirm;
    [SerializeField] private InputActionReference p1GoBack;
    [SerializeField] private InputActionReference p1Ready;
    
    [SerializeField] private InputActionReference p1Jump;
    [SerializeField] private InputActionReference p1Defend;
    

    [Header("Player 2 Input Actions")]
    [SerializeField] private InputActionReference p2MoveRight;
    [SerializeField] private InputActionReference p2MoveLeft;
    [SerializeField] private InputActionReference p2Confirm;
    [SerializeField] private InputActionReference p2GoBack;
    [SerializeField] private InputActionReference p2Ready;
    
    [SerializeField] private InputActionReference p2Jump;
    [SerializeField] private InputActionReference p2Defend;
    

    [Header("Player 1 IA Clips")]
    [SerializeField] private AudioClip _p1MoveRClip;
    [SerializeField] private AudioClip _p1MoveLClip;
    [SerializeField] private AudioClip _p1ConfirmClip;
    [SerializeField] private AudioClip _p1GoBackClip;
    [SerializeField] private AudioClip _p1ReadyClip;
    //[SerializeField] private AudioClip _p1JumpClip;
    //[SerializeField] private AudioClip _p1DefendClip;
    

    [Header("Player 2 IA Clips")]
    [SerializeField] private AudioClip _p2MoveRClip;
    [SerializeField] private AudioClip _p2MoveLClip;
    [SerializeField] private AudioClip _p2ConfirmClip;
    [SerializeField] private AudioClip _p2GoBackClip;
    [SerializeField] private AudioClip _p2ReadyClip;
    //[SerializeField] private AudioClip _p2JumpClip;
    //[SerializeField] private AudioClip _p2DefendClip;

    //[SerializeField] private float jumpClipCooldown = 0.85f;
    

    public static AudioPlayer _instance;

    private Dictionary<SO_SkillStat, AudioClip> _sfxMapping = new Dictionary<SO_SkillStat, AudioClip>();
    /*
    private Dictionary<InputActionReference, bool> IACooldown = new Dictionary<InputActionReference, bool>();
    private List<PlayerInputHandler> playerInputHandlers = new List<PlayerInputHandler>();

    private bool isP1DefendClipPlaying = false;
    private bool isP2DefendClipPlaying = false;
    */

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

    private void Start()
    {
        //Cooldown states for basic actions
        //IACooldown[p1Jump] = true;
        //IACooldown[p2Jump] = true;
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
    
    //Audio IA for basic actions
    /*
    private IEnumerator IAGameCooldown(InputActionReference action)
    {
        IACooldown[action] = false; //disables specific audio
        yield return new WaitForSeconds(jumpClipCooldown); //waits for a certain amount of time
        IACooldown[action] = true; //reenables specific audio
    }
    private void EnableGameMapping()
    {
        EnableGameIA();

        //Player 1 Jump
        p1Jump.action.performed += _ =>
        {
            Debug.Log("Player 1 jump triggered");
            if (IACooldown[p1Jump])
            {
                Public_PlayP1SoundEffect(_p1JumpClip);
                StartCoroutine(IAGameCooldown(p1Jump));
            }
        };

        //Player 2 Jump
        p2Jump.action.performed += _ =>
        {
            Debug.Log("Player 2 jump triggered");
            if (IACooldown[p2Jump])
            {
                Public_PlayP2SoundEffect(_p2JumpClip);
                StartCoroutine(IAGameCooldown(p2Jump));
            }
        };

        //Defend
        foreach (var playerHandler in playerInputHandlers)
        {
            if (playerHandler == null) continue;
            //Player 1 Defend
            p1Defend.action.performed += _ =>
            {
                Debug.Log($"Player 1 defend performed: isDefending = {playerHandler.isDefending}");
                if (playerHandler.isDefending && !isP1DefendClipPlaying)
                {
                    Public_PlayP1SoundEffect(_p1DefendClip);
                    isP1DefendClipPlaying = true;
                }
            };

            p1Defend.action.canceled += _ =>
            {
                isP1DefendClipPlaying = false;
            };

            //Player 2 Defend
            p2Defend.action.performed += _ =>
            {
                Debug.Log($"Player 2 defend performed: isDefending = {playerHandler.isDefending}");
                if (playerHandler.isDefending && !isP2DefendClipPlaying)
                {
                    Public_PlayP2SoundEffect(_p2DefendClip);
                    isP2DefendClipPlaying = true;
                }
            };

            p2Defend.action.canceled += _ =>
            {
                isP2DefendClipPlaying = false;
            };
        }
    }

    private void DisableGameMapping()
    {
        DisableGameIA();
        isP1DefendClipPlaying = false;
        isP2DefendClipPlaying = false;

        //Player 1 Jump
        p1Jump.action.performed -= _ => 
        {
            Debug.Log("Player 1 jump unbounded");
            if (IACooldown[p1Jump]) 
            { 
                Public_PlayP1SoundEffect(_p1JumpClip);
                StartCoroutine(IAGameCooldown(p1Jump));
            } 
        };

        //Player 2 Jump
        p2Jump.action.performed -= _ =>
        {
            Debug.Log("Player 2 jump unbounded");
            if (IACooldown[p2Jump])
            {
                Public_PlayP2SoundEffect(_p2JumpClip);
                StartCoroutine(IAGameCooldown(p2Jump));
            }
        };

        //Defend
        foreach (var playerHandler in playerInputHandlers)
        {
            if (playerHandler == null) continue;
            //Player 1 Defend
            p1Defend.action.performed -= _ =>
            {
                if (playerHandler.isDefending && !isP1DefendClipPlaying)
                {
                    Public_PlayP1SoundEffect(_p1DefendClip);
                    isP1DefendClipPlaying = true;
                }
            };

            p1Defend.action.canceled -= _ =>
            {
                isP1DefendClipPlaying = false;
            };

            //Player 2 Defend
            p2Defend.action.performed -= _ =>
            {
                if (playerHandler.isDefending && !isP2DefendClipPlaying)
                {
                    Public_PlayP2SoundEffect(_p2DefendClip);
                    isP2DefendClipPlaying = true;
                }
            };

            p2Defend.action.canceled -= _ =>
            {
                isP2DefendClipPlaying = false;
            };
        }  
    }

    private void EnableGameIA()
    {
        //Player 1
        p1Jump.action.Enable();
        p1Defend.action.Enable();

        //Player 2
        p2Jump.action.Enable();
        p2Defend.action.Enable();
    }

    private void DisableGameIA()
    {
        //Player 1
        p1Jump.action.Disable();
        p1Defend.action.Disable();

        //Player 2
        p2Jump.action.Disable();
        p2Defend.action.Disable();
    }
    */
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
        
        //Ensuring that audio clips assigned only play if the scene is the game scene
        /*if (_scene.name == "Main-GamePlayer_Scene")
        {
            EnableGameMapping();
        }

        else
        {
            DisableGameMapping();
        }*/
    }

    //Play sounds
>>>>>>> Aaron-Branch
    public void Public_PlaySoundEffect(AudioClip audioClip)
    {
        _sfx.clip = audioClip;
        _sfx.Play();
    }
<<<<<<< HEAD
=======

    //Player 1 basic action play sound
    public void Public_PlayP1SoundEffect(AudioClip audioClip)
    {
        _p1SFX.clip = audioClip;
        _p1SFX.Play();
    }

    //Player 2 basic action play sound
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
>>>>>>> Aaron-Branch
    public void Public_PlayBackgroundMusic(AudioClip audioClip)
    {
        _bgm.clip = audioClip;
        _bgm.Play();
    }
}
