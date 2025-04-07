using System.Collections;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class Game_Manager : MonoBehaviour
{
    [SerializeField] private SO_MatchResult matchResult;
    [SerializeField] private float setUpDuration;
    [SerializeField] private float startDelayDuration;

    [SerializeField] private UnityEvent OnGameSetUpEvent;
    [SerializeField] private UnityEvent OnGameSetUpCompleteEvent;
    [SerializeField] private UnityEvent OnGameStartEvent;
    [SerializeField] private UnityEvent OnMatchEndEvent;

    private PlayerHealthHandler p1Health, p2Health;
    private PlayerInput p1Input, p2Input;
    public static bool IsEndGame;

    private void Start()
    {
        StartCoroutine(SetUpCoroutine());
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    #region ~~ Set up ~~
    public void Public_SetUp(GameObject p1, GameObject p2)
    {
        p1Health = p1.GetComponent<PlayerHealthHandler>();
        p2Health = p2.GetComponent<PlayerHealthHandler>();

        p1Input = p1.GetComponent<PlayerInput>();
        p2Input = p2.GetComponent<PlayerInput>();

        p1Health.OnDeathEvent.AddListener(EndMatch);
        p2Health.OnDeathEvent.AddListener(EndMatch);
    }
    private IEnumerator SetUpCoroutine()
    {
        OnGameSetUpEvent?.Invoke();
        yield return new WaitForSeconds(setUpDuration);
        OnGameSetUpCompleteEvent?.Invoke();
        yield return new WaitForSeconds(startDelayDuration);
        OnGameStartEvent?.Invoke();
    }
    #endregion

    #region ~~ End match ~~
    private void EndMatch()
    { 
        IsEndGame = true;
        p1Input.enabled = false;
        p2Input.enabled = false;
        OnMatchEndEvent?.Invoke();
        StartCoroutine(CalculateMatchResultCoroutine());
    }
    private IEnumerator CalculateMatchResultCoroutine()
    {
        yield return null;  // Delay 1 frame to let isDead booleans from both players update, check in case both players died at the same time 
        if (!IsEndGame)
        {
            if (p1Health.IsDead || p2Health.IsDead)
            {
                if (p1Health.IsDead && p2Health.IsDead)
                    matchResult.Public_OnMatchEnd(0);
                else
                    matchResult.Public_OnMatchEnd(p2Health.IsDead ? 1 : 2);

                ChangeScene();
            }
        }
    }
    private void ChangeScene()
    {
        if (matchResult.GetPlayerOneScore() == 2)
            StartCoroutine(ChangeSceneCoroutine(3, Global.playerOneWinScene));
        else if (matchResult.GetPlayerTwoScore() == 2)
            StartCoroutine(ChangeSceneCoroutine(3, Global.playerTwoWinScene));
        else
            StartCoroutine(ChangeSceneCoroutine(3, Global.gamePlayScene));
    }
    private IEnumerator ChangeSceneCoroutine(float delay, string scene)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(scene);
    }
    #endregion
}
