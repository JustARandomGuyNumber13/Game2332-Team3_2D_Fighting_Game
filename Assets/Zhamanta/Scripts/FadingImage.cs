using UnityEngine;
using UnityEngine.UI;

public class FadingImage : MonoBehaviour
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
  

    // Start is called once before the first execution of Update after the MonoBehaviour is created


    private void Start()
    {
        currentTarget = targetColor1;
        
     
    }
    // Update is called once per frame
    void Update()
    {
        FadeImageLoop();
    }

    private void FadeImageLoop()
    {
        var currentColor = fadeImage.color;
        //var currentTarget = targetColor1;




        //Debug.Log("Loop Started");
        if (currentTarget == targetColor1)
        {
            //Debug.Log("Test1");
            currentColor = Color.Lerp(currentColor, targetColor1, fadeSpeed * Time.deltaTime);
            fadeImage.color = currentColor;
            if (currentColor == targetColor1)
            {
                //Debug.Log("Test2");
                currentTarget = targetColor2;
            }

        }

        if (currentTarget == targetColor2)
        {
            //Debug.Log("Test3");
            currentColor = Color.Lerp(currentColor, targetColor2, fadeSpeed * Time.deltaTime);
            fadeImage.color = currentColor;
            if (currentColor == targetColor2)
            {
                //Debug.Log("Test4");
                currentTarget = targetColor3;
            }

        }

        if (currentTarget == targetColor3)
        {
            //Debug.Log("Test5");
            currentColor = Color.Lerp(currentColor, targetColor3, fadeSpeed * Time.deltaTime);
            fadeImage.color = currentColor;
            if (currentColor == targetColor3)
            {
                //Debug.Log("Test6");
                currentTarget = targetColor1;
            }

        }

    }
}
