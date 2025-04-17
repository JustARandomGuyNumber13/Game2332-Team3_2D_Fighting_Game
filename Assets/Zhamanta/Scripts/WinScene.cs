using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;
using TMPro;
using UnityEngine.UI;

public class WinScene : MonoBehaviour
{
    [SerializeField] private SO_CharactersList characterList;
    [SerializeField] private SO_PlayerSelection p1Selection;

    GameObject player;
    Animator anim;

    [SerializeField] float speedScale;
    [SerializeField] float speedPosition;
    [SerializeField] Vector3 targetScale;
    [SerializeField] Vector3 targetPosition;
    Vector3 originalPosition;

    [SerializeField] TMP_Text winnerText;
    [SerializeField] TMP_Text text1;
    [SerializeField] GameObject rematchButton;
    [SerializeField] GameObject menuButton;

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
    void Start()
    {
        text1.enabled = false;
        rematchButton.SetActive(false);
        menuButton.SetActive(false);

        player = Instantiate(characterList.GetCharacterAt(p1Selection.CharacterIndex).characterPrefab);
        player.transform.position = new Vector3(0, 0, 0);
        originalPosition = player.transform.position;

        anim = player.transform.GetChild(0).GetComponent<Animator>();
        StartCoroutine(AnimationSequence());
        StartCoroutine(RevealText());
    }

    // Update is called once per frame
    void Update()
    {
        if (player.GetComponent<Rigidbody2D>())
        {
            player.GetComponent<Rigidbody2D>().simulated = false;
        }


        player.transform.localScale = Vector3.Lerp(player.transform.localScale, targetScale, Time.deltaTime * speedScale);
        if (player.transform.localScale == targetScale)
        {
            Debug.Log("moving");
            player.transform.position = Vector3.Lerp(player.transform.position, originalPosition + new Vector3(-4, 0, 0), Time.deltaTime * speedPosition);
        }
    }

    IEnumerator AnimationSequence()
    { 
        yield return new WaitForSeconds(1);
        anim.SetTrigger("useSkill");
    }

    IEnumerator RevealText()
    {
        yield return new WaitForSeconds(1);
        text1.enabled = true;

        yield return new WaitForSeconds(1.5f);
        var originalString = "WINNER!";

        var numRevealed = 0;
        while (numRevealed < originalString.Length)
        {
            ++numRevealed;
            winnerText.text = originalString.Substring(0, numRevealed);

            yield return new WaitForSeconds(.3f);
        }

        yield return new WaitForSeconds(1.5f);
        rematchButton.SetActive(true);
        menuButton.SetActive(true);
    }

    /*private void FadeImageLoop()
    {
        var currentColor = fadeImage.color;
        //var currentTarget = targetColor1;


        if (isReady)
        {

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
    }*/
}
