using UnityEngine;
using UnityEngine.UI;

public class AudioMixerSettings : MonoBehaviour
{
    [SerializeField] Slider masterSlider, bgmSlider, sfxSlider;
    [SerializeField] Toggle masterToggle, bgmToggle, sfxToggle;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        InitializeVolume(masterSlider, masterToggle, "MasterVolume");
        InitializeVolume(bgmSlider, bgmToggle, "BGMVolume");
        InitializeVolume(sfxSlider, sfxToggle, "SFXVolume");
    }

    private void InitializeVolume(Slider volSlide, Toggle volMute, string volParameter)
    {
        float volume;
        if (ManageAudio.Instance.mixer.GetFloat(volParameter, out volume))
        {
            volSlide.value = LinearToNormalize(volume);
            volMute.isOn = volume <= -80f;

            volSlide.onValueChanged.AddListener(delegate { SetVolume(volParameter, volSlide.value); });
            volMute.onValueChanged.AddListener(delegate { ToggleMute(volParameter, volMute.isOn, volSlide.value); });
        }

        else
        {
            Debug.LogWarning(volParameter + " parameter not found within the audio mixer!");
        }
    }

    private float LinearToNormalize(float volume)
    {
        return Mathf.Clamp01((volume + 80f) / 80f); //Range from -80 dB to 0 dB
    }

    private float NormalizeToLinear(float volume)
    {
        return Mathf.Lerp(-80f, 0f, volume);
    }

    public void SetVolume(string parameter, float volume)
    {
        float volValue = NormalizeToLinear(volume);
        ManageAudio.Instance.SetVolume(parameter, volValue);
    }

    public void ToggleMute(string parameter, bool isMuted, float volumeValue)
    {
        if (isMuted)
        {
            ManageAudio.Instance.SetVolume(parameter, -80f); //Mute volume
        }

        else
        {
            ManageAudio.Instance.SetVolume(parameter, Mathf.Log10(volumeValue) * 20); //Unmute volume value
        }
    }
}
