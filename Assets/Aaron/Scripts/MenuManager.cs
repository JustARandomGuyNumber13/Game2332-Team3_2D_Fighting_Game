using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    //public / Serialized
    [SerializeField] GameObject audioSettingsUI;
    [SerializeField] GameObject mainMenuUI;

    //private
    private Audio_Manager audioManager;

    private void Start()
    {
        audioManager = FindObjectOfType<Audio_Manager>();

        if (audioManager == null)
        {
            Debug.Log("Audio_Manager isntance does not exist");
        }
    }

    public void PlayButton()
    {
        //SceneManager.LoadScene(); //Where the game will open the player selection UI scene
    }

    public void SettingsButton()
    {
        audioSettingsUI.SetActive(true);
        mainMenuUI.SetActive(false);

        //Shows song name display when settings button is clicked on
        if (audioManager != null)
        {
            audioManager.ShowBGMname();
        }
    }

    public void QuitButton()
    {
#if UNITY_EDITOR
        //Runs this if playing within the Unity Editor
        UnityEditor.EditorApplication.isPlaying = false;
#else
        //Runs this if playing on a build of the game. Not sure if we might need this in the future
        Application.Quit();
#endif
    }

    public void BackToMenuButton()
    {
        audioSettingsUI.SetActive(false);
        mainMenuUI.SetActive(true);
    }
}
