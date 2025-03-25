using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class T_Scene_Manager : MonoBehaviour
{

    public void UI_GoToSelectionScene(float delay)
    {
        StartCoroutine(GoToSceneCoroutine(delay, Global.skillSelectionScene));
    }
    public void UI_GoToGamePlayScene(float delay)
    {
        StartCoroutine(GoToSceneCoroutine(delay, Global.gamePlayScene));
    }


    private IEnumerator GoToSceneCoroutine(float delay, string sceneName)
    { 
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(sceneName);
    }
    public void UI_ExitGame()
    { 
        Application.Quit();
    }
}
