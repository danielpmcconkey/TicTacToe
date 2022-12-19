using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Assets.Scripts;

public class Marquee : MonoBehaviour
{
    #region properties exposed to the inspector
    public TextMeshProUGUI textObject;
    #endregion

    #region MonoBehaviour Methods
    private void Start()
    {
        textObject.text = "Ready Player 1.";
        BoardEngine.OnTurnEnded += UpdateMarquee;
        ResetButton.OnResetClicked += ResetGame;
    }
    private void Update()
    {
        
    }
    #endregion

    #region private methods
    private void ResetGame()
    {
        textObject.text = "Welcome to New Game Plus! Player 1's turn.";
    }
    private void UpdateMarquee(GameState gameState)
    {
        switch(gameState)
        {
            case GameState.PLAYER_1_TURN:
                textObject.text = "Player 1's turn.";
                break;
            case GameState.PLAYER_2_TURN:
                textObject.text = "Player 2's turn.";
                break;
            case GameState.PLAYER_1_WINS:
                textObject.text = "Player 1 wins! Hail to the King, baby!";
                break;
            case GameState.PLAYER_2_WINS:
                textObject.text = "Fatality! Player 2 wins!";
                break;
            case GameState.DRAW:
                textObject.text = "Oh no, we suck again!";
                break;

        }
    }
    #endregion
}
