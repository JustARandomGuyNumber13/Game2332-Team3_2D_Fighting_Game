using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Test : MonoBehaviour
{
    [SerializeField] private TMP_Text debugText;

    public static TMP_Text m_debugText;

    private void Awake()
    {
        m_debugText = debugText;
    }

    public void Public_TimeScale(float value)
    { Time.timeScale = value; }
}
