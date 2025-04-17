using UnityEngine;
using UnityEngine.UI;

public class FadeImage : MonoBehaviour
{

    [SerializeField]
    private Image fadeImage;
    [SerializeField]
    Color targetColor1;
    [SerializeField]
    Color targetColor2;
    [SerializeField]
    Color targetColor3;
    [SerializeField]
    float fadeSpeed;
    Color currentTarget;


    // Update is called once per frame
    void Update()
    {
        FadeImageLoop();
    }

    private void FadeImageLoop()
    {
        var currentColor = fadeImage.color;
        //var currentTarget = targetColor1;


  

            Debug.Log("Loop Started");
            if (currentTarget == targetColor1)
            {
                currentColor = Color.Lerp(currentColor, targetColor1, fadeSpeed * Time.deltaTime);
                fadeImage.color = currentColor;
                if (currentColor == targetColor1)
                {
                    currentTarget = targetColor2;
                }

            }

            if (currentTarget == targetColor2)
            {
                currentColor = Color.Lerp(currentColor, targetColor2, fadeSpeed * Time.deltaTime);
                fadeImage.color = currentColor;
                if (currentColor == targetColor2)
                {
                    currentTarget = targetColor3;
                }

            }

            if (currentTarget == targetColor3)
            {
                currentColor = Color.Lerp(currentColor, targetColor3, fadeSpeed * Time.deltaTime);
                fadeImage.color = currentColor;
                if (currentColor == targetColor3)
                {
                    currentTarget = targetColor1;
                }

            }
        
    }
}
