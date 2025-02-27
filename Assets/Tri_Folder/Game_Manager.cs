using UnityEngine;
using UnityEngine.Events;

public class Game_Manager : MonoBehaviour
{
    [SerializeField] private SO_MatchResult _matchResult;
    //[SerializeField] private Trap[] _trapList;

    public UnityEvent OnMatchEnd;

    private PlayerHealthHandler _playerOneHealthHandler, _playerTwoHealthHandler;
    private float _matchTimer;

    private void Start()
    {
        
    }
    private void Update()
    {
        _matchTimer += Time.deltaTime;
    }

    private void SpawnTraps()
    {
        //float randomChosenTrap = Random.Range(0, _trapList.Length);
        //while (true)
        //{
        //    if (_trapList[randomChosenTrap].IsAvailable)
        //        _trapList[randomChosenTrap].Activate();
        //    else
        //    {
        //        randomChosenTrap++;
        //        if (randomChosenTrap >= _trapList.Length)
        //            randomChoseTrap = 0;
        //    }
        //}
    }
    private void SpawnDeathWall()
    { 
        // TODO: Implement spawn Death Wall
    }

    public void Public_SetUp(PlayerHealthHandler p1Health, PlayerHealthHandler p2Health)
    {
        _playerOneHealthHandler = p1Health;
        _playerTwoHealthHandler = p2Health;
    }
    //public void Public_OnPlayerHealthChange()
    //{
    //    if (_playerOneHealthHandler.IsDead && _playerTwoHealthHandler.IsDead)
    //    {
    //        _matchResult.Public_OnMatchEnd(0);
    //        OnMatchEnd?.Invoke();
    //        CheckPlayerWin();
    //    }
    //    else if (_playerOneHealthHandler.IsDead || _playerTwoHealthHandler.IsDead)
    //    { 
    //        _matchResult.Public_OnMatchEnd(_playerTwoHealthHandler.IsDead ? 1 : 2);
    //        OnMatchEnd?.Invoke();
    //        CheckPlayerWin();
    //    }
    //}

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
}
