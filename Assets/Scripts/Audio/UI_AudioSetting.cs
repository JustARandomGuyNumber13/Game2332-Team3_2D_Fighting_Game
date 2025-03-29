using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Audio;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

public class UI_AudioSetting : MonoBehaviour
{
    [SerializeField] private AudioMixer _mixer;

    [SerializeField] private Slider _masterSlider;
    [SerializeField] private Slider _bgmSlider;
    [SerializeField] private Slider _sfxSlider;

    private void Start()
    {
        SetUp();
    }
    private void SetUp()
    {
        float masterVolume = 0;
        _mixer.GetFloat("Master", out masterVolume);
        _masterSlider.value = DecibelToLinear(masterVolume);
        _masterSlider.onValueChanged.AddListener(SetMasterVolume);

        float bgmVolume = 0;
        _mixer.GetFloat("Bgm", out bgmVolume);
        _bgmSlider.value = DecibelToLinear(bgmVolume);
        _bgmSlider.onValueChanged.AddListener(SetBgmVolume);

        float sfxVolume = 0;
        _mixer.GetFloat("Sfx", out sfxVolume);
        _sfxSlider.value = DecibelToLinear(sfxVolume);
        _sfxSlider.onValueChanged.AddListener(SetSfxVolume);
    }
    public void UI_OnReturnButtonPress()
    {

        if (SceneManager.GetActiveScene().name == "Test 1-MainMenu")
            //SceneManager.LoadScene("Test 2-GamePlay");
            //SceneManager.LoadScene("Character-Skill Selection Scene");
            SceneManager.LoadScene("Main-GamePlayer_Scene");
        else if (SceneManager.GetActiveScene().name == "Test 2-GamePlay")
            SceneManager.LoadScene("Test 1-MainMenu");
    }
    private void SetMasterVolume(float value)
    {
        //_mixer.SetFloat("Master", value * Mathf.Log10(value) * 20);
        _mixer.SetFloat("Master", Mathf.Log10(Mathf.Clamp(value, 0.0001f, 1f)) * 20);
    }
    private void SetBgmVolume(float value)
    {
        //_mixer.SetFloat("Bgm", value * Mathf.Log10(value) * 20);
        _mixer.SetFloat("Bgm", Mathf.Log10(Mathf.Clamp(value, 0.0001f, 1f)) * 20);
    }
    private void SetSfxVolume(float value)
    {
        //_mixer.SetFloat("Sfx", value * Mathf.Log10(value) * 20);
        _mixer.SetFloat("Sfx", Mathf.Log10(Mathf.Clamp(value, 0.0001f, 1f)) * 20);
    }

    private void OnDestroy()
    {
        _masterSlider.onValueChanged.RemoveListener(SetMasterVolume);
        _bgmSlider.onValueChanged.RemoveListener(SetBgmVolume);
        _sfxSlider.onValueChanged.RemoveListener(SetSfxVolume);

    }

    private float DecibelToLinear(float decibel)
    {
        return Mathf.Pow(10, decibel / 20);
    }
}
