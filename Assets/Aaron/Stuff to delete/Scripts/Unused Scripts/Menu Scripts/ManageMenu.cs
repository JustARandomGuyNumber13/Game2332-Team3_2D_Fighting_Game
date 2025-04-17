using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ManageMenu : MonoBehaviour
{
    [SerializeField] GameObject mainMenuUI;

    public void PlayButton()
    {
        //SceneManager.LoadScene(); //Where the game will open the player selection UI scene
        SceneManager.LoadScene("Tri_Scene (Copy)");
    }

    public void SettingsButton()
    {
        SceneManager.LoadScene("VolumeScene");
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

    public void BackToMenu()
    {
        SceneManager.LoadScene("Menu");
        SceneManager.UnloadSceneAsync("VolumeScene");
    }
}
