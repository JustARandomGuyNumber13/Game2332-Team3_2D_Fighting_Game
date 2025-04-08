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

    [SerializeField] private Toggle _masterToggle;
    [SerializeField] private Toggle _bgmToggle;
    [SerializeField] private Toggle _sfxToggle;


    private void Start()
    {
        SetUp();
    }
    private void SetUp()
    {
        float masterVolume = 0;
        _mixer.GetFloat("Master", out masterVolume);
        _masterSlider.value = masterVolume;
        _masterSlider.onValueChanged.AddListener(SetMasterVolume);

        float bgmVolume = 0;
        _mixer.GetFloat("Bgm", out bgmVolume);
        _bgmSlider.value = bgmVolume;
        _bgmSlider.onValueChanged.AddListener(SetBgmVolume);

        float sfxVolume = 0;
        _mixer.GetFloat("Sfx", out sfxVolume);
        _sfxSlider.value = sfxVolume;
        _sfxSlider.onValueChanged.AddListener(SetSfxVolume);
    }
    public void UI_OnReturnButtonPress()
    {

        if (SceneManager.GetActiveScene().name == "Test 1-MainMenu")
            SceneManager.LoadScene("Test 2-GamePlay");
        else if (SceneManager.GetActiveScene().name == "Test 2-GamePlay")
            SceneManager.LoadScene("Test 1-MainMenu");
    }
    private void SetMasterVolume(float value)
    {
        _mixer.SetFloat("Master", value * Mathf.Log10(value) * 20);
    }
    private void SetBgmVolume(float value)
    {
        _mixer.SetFloat("Bgm", value * Mathf.Log10(value) * 20);
    }
    private void SetSfxVolume(float value)
    {
        _mixer.SetFloat("Sfx", value * Mathf.Log10(value) * 20);
    }
}
