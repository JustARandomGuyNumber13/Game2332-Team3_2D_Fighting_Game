using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "SO_MatchResult", menuName = "Scriptable Objects/SO_MatchResult")]
public class SO_MatchResult : ScriptableObject
{
    private int _playerOneScore, _playerTwoScore;

    public void Public_OnMatchEnd(int winCase)
    {
        if (winCase == 1)
            _playerOneScore++;
        else if(winCase == 2)
            _playerTwoScore++;
    }
    public void Public_ResetScore()
    { 
        _playerOneScore = 0;
        _playerTwoScore = 0;
    }
    public int GetPlayerOneScore() { return _playerOneScore; }
    public int GetPlayerTwoScore() { return _playerTwoScore; }
}
