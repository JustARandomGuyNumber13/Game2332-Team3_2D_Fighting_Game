using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;

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

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
        player = Instantiate(characterList.GetCharacterAt(p1Selection.CharacterIndex).characterPrefab);
        originalPosition = player.transform.position;

        anim = player.transform.GetChild(0).GetComponent<Animator>();
        StartCoroutine(AnimationSequence());
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
            player.transform.position = Vector3.Lerp(player.transform.position, originalPosition + new Vector3(-5, 0, 0), Time.deltaTime * speedPosition);
        }

    }

    IEnumerator AnimationSequence()
    { 
        yield return new WaitForSeconds(1);
        anim.SetTrigger("useSkill");
    }
}
