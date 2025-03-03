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
        InitializeSettings();
    }

    private void OnEnable()
    {
        InitializeSettings();
    }
    private void InitializeSettings()
    {
        if (Audio_Manager.Instance == null) //Making sure that the instance exists
        {
            Debug.Log("AudioManager Instance not present");
            return;
        }

        InitializeUIElements(); //Attaching Ui elements to Audio_Manager values
        AddUIListener(); //Adding listeners to try to update settings when there is a UI change
        Audio_Manager.Instance.UpdateUIComponents(bgmSlider, sfxSlider, bgmToggle, sfxToggle);
    }
    private void InitializeUIElements()
    {
        //Setting UI elements to audio manager script elements
        if (bgmSlider != null)
        {
            bgmSlider.value = Audio_Manager.Instance.bgmSlide.value;
        }

        if (sfxSlider != null)
        {
            sfxSlider.value = Audio_Manager.Instance.sfxSlide.value;
        }

        if (bgmToggle != null)
        {
            bgmToggle.isOn = Audio_Manager.Instance.bgmToggle.isOn;
        }

        if (sfxToggle != null)
        {
            sfxToggle.isOn = Audio_Manager.Instance.sfxToggle.isOn;
        }
    }

    private void AddUIListener()
    {
        //Listeners for updating inactive audio manager UI sliders and toggles
        if (bgmSlider != null)
        {
            bgmSlider.onValueChanged.AddListener(value =>
            {
                Audio_Manager.Instance.bgmSlide.value = value;
                Audio_Manager.Instance.SetBGMVol(value);
            });
        }

        if (sfxSlider != null)
        {
            sfxSlider.onValueChanged.AddListener(value =>
            {
                Audio_Manager.Instance.sfxSlide.value = value;
                Audio_Manager.Instance.SetSFXVol(value);
            });
        }

        if (bgmToggle != null)
        {
            bgmToggle.onValueChanged.AddListener(isOn =>
            {
                Audio_Manager.Instance.bgmToggle.isOn = isOn;
                Audio_Manager.Instance.ToggleBGM(isOn);
            });
        }

        if (sfxToggle != null)
        {
            sfxToggle.onValueChanged.AddListener(isOn =>
            {
                Audio_Manager.Instance.sfxToggle.isOn = isOn;
                Audio_Manager.Instance.ToggleSFX(isOn);
            });
        }
    }
}
