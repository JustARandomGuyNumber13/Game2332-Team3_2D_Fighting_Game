using UnityEngine;
using UnityEngine.Audio;

public class MasterAudio_Test : MonoBehaviour
{
    [SerializeField] private AudioMixer _masterAudio;
    [SerializeField] private AudioSource _bgm, _sfx;

    private static MasterAudio_Test masterAudioInstance;

    private void Start()
    {
        if (masterAudioInstance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            masterAudioInstance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public void Public_PlaySoundEffect(AudioClip audioClip)
    {
        _sfx.clip = audioClip;
        _sfx.Play();
    }
    public void Public_PlayBackgroundMusic(AudioClip audioClip)
    {
        _bgm.clip = audioClip;
        _bgm.Play();
    }

    public void UI_SetBgmVolume(float value)
    { }
    public void UI_SetSfxVolume(float value)
    { }
}
