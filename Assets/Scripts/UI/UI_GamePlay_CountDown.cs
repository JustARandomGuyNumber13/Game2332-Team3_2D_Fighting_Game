using UnityEngine;
using TMPro;
using System.Collections;

public class UI_GamePlay_CountDown : MonoBehaviour
{
    [SerializeField] private TMP_Text countDownText;
    [SerializeField] private float countDownDelay;
    [SerializeField] private int countDownDuration;
    private Animator countDownAnim;

    private void Awake()
    {
        countDownAnim = countDownText.GetComponent<Animator>();
    }

    public void Public_StartCountDown()
    { 
        StartCoroutine(StartCountDownCoroutine());
    }
    private IEnumerator StartCountDownCoroutine()
    {
        yield return new WaitForSeconds(countDownDelay);
        
        countDownText.gameObject.SetActive(true);
        int t = countDownDuration;

        while (t > -1)
        {
            countDownAnim.SetTrigger("trigger");
            countDownText.text = t.ToString();
            if (t == 0)
                countDownText.text = "Fight!";

            t--;
            yield return new WaitForSeconds(1);
        }
        countDownText.gameObject.SetActive(false);
    }
}
