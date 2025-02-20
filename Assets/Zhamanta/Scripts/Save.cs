using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Events;
using TMPro;
using UnityEngine.UI;

//public bool _isReady;


public class Save : MonoBehaviour

{
    public UnityEvent OnReadyCheck;

    [SerializeField] private SO_PlayerSelection player1Selection, player2Selection;

    private bool isReady1;
    private bool isReady2;
    /*public Text readyText1;
    public Text readyText2;*/

    public void ReadyCheck()
    {
        /*if (player1)
        {
            isReady1 = isReady1 ? false : true;
            readyText1.enabled = isReady1 ? true : false;
        }

        else
        {
            isReady2 = isReady2 ? false : true;
            readyText2.enabled = isReady2 ? true : false;
        }*/

        Debug.Log(isReady1 + " " + isReady2);

        if (isReady1 && isReady2)
        {
            Debug.Log("Data is saved");
            //change scene
        }
    }

    private void Start()
    {
        /*readyText1.enabled = false;
        readyText2.enabled = false;*/

        OnReadyCheck.AddListener(ReadyCheck);
    }

    //if both players are ready
    //SaveData(int player 1charIndex, int player 1skillOne, int skillTwo, int skillThree)
    //SaveData(int player 2charIndex, int player 2skillOne, int skillTwo, int skillThree)
}



