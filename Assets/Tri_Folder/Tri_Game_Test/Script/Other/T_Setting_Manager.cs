using UnityEngine;

public class T_Setting_Manager : MonoBehaviour
{
    [SerializeField] private GameObject _audioSettingCanvasPrefab;
    [SerializeField] private GameObject _keyBindingCanvasPrefab;

    public void Public_EnterSettingCanvas()
    {
        _audioSettingCanvasPrefab.SetActive(true);
        _keyBindingCanvasPrefab.SetActive(false);
    }
    public void Public_EnterKeyBindingCanvas()
    {
        _audioSettingCanvasPrefab.SetActive(false);
        _keyBindingCanvasPrefab.SetActive(true);
    }
    public void Public_ExitSettingCanvas()
    {
        _audioSettingCanvasPrefab.SetActive(false);
        _keyBindingCanvasPrefab.SetActive(false);
    }
}
