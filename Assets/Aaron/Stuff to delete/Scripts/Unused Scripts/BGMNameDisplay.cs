using UnityEngine;
using TMPro;
using System.Collections;

public class BGMNameDisplay : MonoBehaviour
{   
    //public
    public RectTransform panel; //UI part for song name
    public TextMeshProUGUI songnameText; //Variable to display song's name
    public float slideDuration = 0.5f; //Duration of the slide animation
    public float displayTime = 2f; //Time where the song's name is displayed

    //private
    private Vector2 offScreenPos;
    private Vector2 onScreenPos;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        offScreenPos = new Vector2(Screen.width + panel.rect.width, panel.anchoredPosition.y); //off screen on right side of panel
        onScreenPos = new Vector2(Screen.width -panel.rect.width - 5, panel.anchoredPosition.y); //Top corner where the name display will be positioned

        panel.anchoredPosition = offScreenPos; //start panel off screen/canvas
        panel.gameObject.SetActive(false);
    }

    public void DisplaySongName(string songName)
    {
        try
        {
            songnameText.text = songName;
            panel.gameObject.SetActive(true);
            StartCoroutine(SongNameSlide());
        }

        catch (System.Exception e)
        {
            Debug.Log("Unable to display song name: " + e.Message);
        }
    }

    private IEnumerator SongNameSlide()
    {
        //Slide in
        float elapsedTime = 0;
        while (elapsedTime < slideDuration)
        {
            panel.anchoredPosition = Vector2.Lerp(offScreenPos, onScreenPos, elapsedTime / slideDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        panel.anchoredPosition = onScreenPos;

        //Wait display time
        yield return new WaitForSeconds(displayTime);

        //Slide out
        elapsedTime = 0;
        while (elapsedTime < slideDuration)
        {
            panel.anchoredPosition = Vector2.Lerp(onScreenPos, offScreenPos, elapsedTime / slideDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        panel.anchoredPosition = offScreenPos;

        panel.gameObject.SetActive(false);
    }
}
