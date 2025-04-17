using UnityEngine;

public class SFXManager : MonoBehaviour
{
    public static SFXManager Instance;
    [SerializeField] private AudioClip[] sfxClips;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }

        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public void PlaySFX(int index)
    {
        if (index >= 0 && index < sfxClips.Length)
        {
            if (ManageAudio.Instance != null)
            {
                ManageAudio.Instance.PlaySFX(sfxClips[index]);
            }
            else
            {
                Debug.LogError("Audio Manger instance is null!");
            }
        }
        else
        {
            Debug.Log("SFX index out of range!");
        }
    }
}
