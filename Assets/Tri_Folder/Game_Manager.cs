using System.Collections;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class Game_Manager : MonoBehaviour
{
    [SerializeField] private SO_MatchResult _matchResult;
    [SerializeField] private Trap[] trapList;

    public UnityEvent OnMatchEndEvent;
    public float MatchTimer { get; private set; }

    private PlayerHealthHandler _p1HealthHandler, _p2HealthHandler;
    private bool _isEndGame;
    

    private void Update()
    {
        MatchTimer += Time.deltaTime;
    }

    private void SpawnTraps()
    {
        if (trapList.Length == 0) return;
        int randomChosenTrap = (int)Random.Range(0, trapList.Length);

        while (true)
        {
            if (trapList[randomChosenTrap].IsAvailable)
                trapList[randomChosenTrap].Activate();
            else
            {
                randomChosenTrap++;
                if (randomChosenTrap >= trapList.Length)
                    randomChosenTrap = 0;
            }
        }
    }
    private void SpawnDeathWall()
    { 
        // TODO: Implement spawn Death Wall
    }

    private void CheckPlayerWin()
    {
        if (_matchResult.GetPlayerOneScore() == 2)
        {
            StartCoroutine(ChangeSceneCoroutine(3, Global.playerOneWinScene));
        }
        else if (_matchResult.GetPlayerTwoScore() == 2)
        {
            StartCoroutine(ChangeSceneCoroutine(3, Global.playerTwoWinScene));
        }
        else
        {
            StartCoroutine(ChangeSceneCoroutine(3, Global.gamePlayScene));
        }
    }
    private IEnumerator ChangeSceneCoroutine(float delay, string scene)
    { 
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(scene);
    }

    public void Public_SetUp(GameObject p1, GameObject p2)
    {
        _p1HealthHandler = p1.GetComponent<PlayerHealthHandler>();
        _p2HealthHandler = p2.GetComponent<PlayerHealthHandler>();

        _p1HealthHandler.OnDeathEvent.AddListener(OnPlayerDieCheck);
        _p2HealthHandler.OnDeathEvent.AddListener(OnPlayerDieCheck);
    }
    private void OnPlayerDieCheck()
    { StartCoroutine(OnPlayerDieCheckCoroutine()); }
    private void OnPlayerDie()
    {
        _isEndGame = true;
        OnMatchEndEvent?.Invoke();
    }
    private IEnumerator OnPlayerDieCheckCoroutine()
    {
        yield return null;  // Delay 1 frame to let isDead booleans from both players update, check in case both players died at the same time 
        if (_p1HealthHandler.IsDead || _p2HealthHandler.IsDead) // 1 Player Win
        {
            if (_p1HealthHandler.IsDead && _p2HealthHandler.IsDead)
                _matchResult.Public_OnMatchEnd(0);
            else
                _matchResult.Public_OnMatchEnd(_p2HealthHandler.IsDead ? 1 : 2);

            OnPlayerDie();
            CheckPlayerWin();
        }
    }
    public void Public_StartMatchTimer()
    {
        if(MatchTimer == 0)
            StartCoroutine(MatchTimerCoroutine());
    }
    private IEnumerator MatchTimerCoroutine()
    {
        while (!_isEndGame)
        {
            MatchTimer++;
            yield return new WaitForSeconds(1);
            // TODO: Implement Tick Events
        }
    }
}
