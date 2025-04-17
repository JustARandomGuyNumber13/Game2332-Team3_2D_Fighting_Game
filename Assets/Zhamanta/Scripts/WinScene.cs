using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class WinScene : MonoBehaviour
{
    [SerializeField] private SO_CharactersList characterList;
    [SerializeField] private SO_PlayerSelection p1Selection, p2Selection;

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

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        text1.enabled = false;
        rematchButton.SetActive(false);
        menuButton.SetActive(false);

        if (SceneManager.GetActiveScene().name == Global.player1WinScene)
        {
            player = Instantiate(characterList.GetCharacterAt(p1Selection.CharacterIndex).characterPrefab);
        }
        else if (SceneManager.GetActiveScene().name == Global.player2WinScene)
        {
            player = Instantiate(characterList.GetCharacterAt(p2Selection.CharacterIndex).characterPrefab);
        }

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
}
