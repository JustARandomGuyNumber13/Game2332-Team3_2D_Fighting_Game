using UnityEngine;
using UnityEngine.UI;

public class AudioSettings : MonoBehaviour
{
    public Slider bgmSlider;
    public Slider sfxSlider;
    public Toggle bgmToggle;
    public Toggle sfxToggle;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (Audio_Manager.Instance == null) //Making sure that the instance exists
        {
            Debug.Log("AudioManager Insatance not present");
            return;
        }

        //Setting UI elements to audio manager script elements
        bgmSlider.value = Audio_Manager.Instance.bgmSlide.value;
        sfxSlider.value = Audio_Manager.Instance.sfxSlide.value;
        bgmToggle.isOn = Audio_Manager.Instance.bgmToggle.isOn;
        sfxToggle.isOn = Audio_Manager.Instance.sfxToggle.isOn;

        //Listeners for updating inactive audio manager UI sliders and toggles
        bgmSlider.onValueChanged.AddListener(value => {
            Audio_Manager.Instance.bgmSlide.value = value;
            Audio_Manager.Instance.SetBGMVol(value);
        });
        sfxSlider.onValueChanged.AddListener(value => {
            Audio_Manager.Instance.sfxSlide.value = value;
            Audio_Manager.Instance.SetSFXVol(value);
        });
        bgmToggle.onValueChanged.AddListener(isOn => {
            Audio_Manager.Instance.bgmToggle.isOn = isOn;
            Audio_Manager.Instance.ToggleBGM(isOn);
        });
        sfxToggle.onValueChanged.AddListener(isOn => {
            Audio_Manager.Instance.sfxToggle.isOn = isOn;
            Audio_Manager.Instance.ToggleSFX(isOn);
        });
    }
}
