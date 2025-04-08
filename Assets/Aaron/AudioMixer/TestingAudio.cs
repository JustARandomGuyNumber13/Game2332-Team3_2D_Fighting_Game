using UnityEngine;

public class TestingAudio : MonoBehaviour
{
    public AudioClip sceneBGM, sceneSFX;

    void Start()
    {
        if (sceneBGM != null)
        {
            ManageAudio.Instance.PlayBGM(sceneBGM);
        }

        if (sceneSFX != null)
        {
            ManageAudio.Instance.PlaySFX(sceneSFX);
        }
    }
}
