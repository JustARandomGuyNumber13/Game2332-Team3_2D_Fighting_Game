using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

[System.Serializable]
public class SkillAudioMapping
{
    public SO_SkillStat skillStat;
    public AudioClip sfxClip;
}

public class AudioPlayer : MonoBehaviour
{
    [SerializeField] private AudioSource _bgm, _sfx;
    [SerializeField] private AudioClip _menuBGM, _gameBGM;
    [SerializeField] private SkillAudioMapping[] skillAudioMappings;

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
                Debug.LogWarning("Skill or SFX is missing in mapping!");
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
}
