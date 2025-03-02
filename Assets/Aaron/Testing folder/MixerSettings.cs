using UnityEngine;
using UnityEngine.UI;

public class MixerSettings : MonoBehaviour
{
    [SerializeField] Slider masterSlide, bgmSlide, sfxSlide;
    [SerializeField] Toggle masterMute, bgmMute, sfxMute;

    private void Start()
    {
        Initialize(masterSlide, masterMute, "MasterVolume");
        Initialize(bgmSlide, bgmMute, "BGMVolume");
        Initialize(sfxSlide, sfxMute, "SFXVolume");
    }

    private void Initialize(Slider volSlider, Toggle volMute, string volParameter)
    {
        float volume;
        if (ManageAudio.Instance.mixer.GetFloat(volParameter, out volume))
        {
            volSlider.value = Mathf.Pow(10, volume / 20);
            volMute.isOn = volume == -80f;

            volSlider.onValueChanged.AddListener(delegate { SetVolume(volParameter, volSlider.value); });
            volMute.onValueChanged.AddListener(delegate { ToggleMute(volParameter, volMute.isOn, volSlider.value); });
        }
        else
        {
            Debug.LogWarning(volParameter + " parameter not found in Audio Mixer");
        }
    }

    public void SetVolume(string parameter, float volume)
    {
        if (parameter == "MasterVolume")
        {
            ManageAudio.Instance.SetMasterVolume(volume);
        }
        else if (parameter == "BGMVolume")
        {
            ManageAudio.Instance.SetBGMVolume(volume);
        }
        else if (parameter == "SFXVolume")
        {
            ManageAudio.Instance.SetSFXVolume(volume);
        }
    }

    public void ToggleMute(string parameter, bool isMuted, float volumeSliderValue)
    {
        if (isMuted)
        {
            ManageAudio.Instance.Mute(parameter); // Mute
        }
        else
        {
            SetVolume(parameter, volumeSliderValue); // Unmute and set to slider value
        }
    }
}
