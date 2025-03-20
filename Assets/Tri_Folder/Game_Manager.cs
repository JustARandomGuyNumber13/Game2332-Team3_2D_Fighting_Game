using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class Game_Manager : MonoBehaviour
{
    [SerializeField] private SO_MatchResult _matchResult;
    //[SerializeField] private Trap[] _trapList;

    public UnityEvent OnMatchEndEvent;
    public float MatchTimer { get; private set; }

    private PlayerHealthHandler _p1HealthHandler, _p2HealthHandler;
    private PlayerInput _p1Input, _p2Input;
    private bool _isEndGame;
    

    private void Start()
    {
        
    }
    private void Update()
    {
        MatchTimer += Time.deltaTime;
    }

    private struct Trap {
        public bool IsAvailable { get; private set; }
        public void Activate() { }
    };
    private Trap[] _trapList;
    private void SpawnTraps()
    {
        int randomChosenTrap = (int)Random.Range(0, _trapList.Length);
        while (true)
        {
            if (_trapList[randomChosenTrap].IsAvailable)
                _trapList[randomChosenTrap].Activate();
            else
            {
                randomChosenTrap++;
                if (randomChosenTrap >= _trapList.Length)
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
            // TODO: Go to player 1 win scene
        }
        else if (_matchResult.GetPlayerTwoScore() == 2)
        {
            // TODO : Go to player 2 win scene
        }
        else
        { 
            // TODO: Repeat fight match scene
        }
    }


    public void Public_SetUp(GameObject p1, GameObject p2)
    {
        _p1HealthHandler = p1.GetComponent<PlayerHealthHandler>();
        _p2HealthHandler = p2.GetComponent<PlayerHealthHandler>();

        _p1Input = p1.GetComponent<PlayerInput>();
        _p2Input = p2.GetComponent<PlayerInput>();

        _p1HealthHandler.OnDeathEvent.AddListener(OnPlayerDieCheck);
        _p2HealthHandler.OnDeathEvent.AddListener(OnPlayerDieCheck);
    }
    private void OnPlayerDieCheck()
    { StartCoroutine(OnPlayerDieCheckCoroutine()); }
    private void OnPlayerDie()
    {
        _isEndGame = true;
        _p1Input.enabled = false;
        _p2Input.enabled = false;
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
