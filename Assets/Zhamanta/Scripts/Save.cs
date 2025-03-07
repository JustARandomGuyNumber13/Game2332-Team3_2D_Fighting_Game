using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Events;
using TMPro;
using UnityEngine.UI;
using TMPro.EditorUtilities;

//public bool _isReady;


public class Save : MonoBehaviour

{
    [SerializeField] private SelectionUI selectionUI;
    [SerializeField] private SO_PlayerSelection SO_player1, SO_player2;

    /*private bool isReady1;
    private bool isReady2;
    *//*public Text readyText1;
    public Text readyText2;*/

    /*public void ReadyCheck()
    {
        *//*if (player1)
        {
            isReady1 = isReady1 ? false : true;
            readyText1.enabled = isReady1 ? true : false;
        }

        else
        {
            isReady2 = isReady2 ? false : true;
            readyText2.enabled = isReady2 ? true : false;
        }*/

        /*Debug.Log(isReady1 + " " + isReady2);

        if (isReady1 && isReady2)
        {
            Debug.Log("Data is saved");
            //change scene
        }*//*
    }*/

    public void SaveAndChange(MyCharacterSelection player1Data, MyCharacterSelection player2Data)
    {
        SO_player1.SaveData(player1Data.characterSelectionIndex, player1Data.skill1Index, player1Data.skill2Index, player1Data.skill3Index);
        SO_player2.SaveData(player2Data.characterSelectionIndex, player2Data.skill1Index, player2Data.skill2Index, player2Data.skill3Index);
        //Change scene (Load(fight scene))
    }

    private void OnEnable()
    {
        selectionUI.OnReady.AddListener(SaveAndChange);
    }

    //if both players are ready
    //SaveData(int player 1charIndex, int player 1skillOne, int skillTwo, int skillThree)
    //SaveData(int player 2charIndex, int player 2skillOne, int skillTwo, int skillThree)
}



