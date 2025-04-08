using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class UI_TransitionEffect_FadeOut : MonoBehaviour
{
    private Image _transitionImage;
    private Color _curColor;


    private void Awake()
    {
        _transitionImage = GetComponent<Image>();
        _transitionImage.enabled = true;
        _curColor = _transitionImage.color;
    }
    public void Public_FadeOutEffect(float duration)
    {
        StartCoroutine(FadeInCoroutine(duration, 0.01f));
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
