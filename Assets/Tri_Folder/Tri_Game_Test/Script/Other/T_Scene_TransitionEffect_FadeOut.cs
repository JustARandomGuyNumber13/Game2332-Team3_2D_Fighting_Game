using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class T_Scene_TransitionEffect_FadeOut : MonoBehaviour
{
    [SerializeField] private float _duration;
    private Image _transitionImage;
    private Color _curColor;

    private void Start()
    {
        _transitionImage = GetComponent<Image>();
        _transitionImage.enabled = true;
        _curColor = _transitionImage.color;
        StartCoroutine(FadeInCoroutine(_duration, 0.01f));
    }
    private IEnumerator FadeInCoroutine(float duration, float tick)
    {
        float changeValue = tick / duration;
        while (duration > 0)
        {
            yield return new WaitForSeconds(tick);
            duration -= tick;
            _curColor.a -= changeValue;
            _transitionImage.color = _curColor;
        }
        _transitionImage.enabled = false;
    }
}
