using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SquareButton : MonoBehaviour, IPointerClickHandler
{
    #region properties exposed to the inspector
    public int boardPosition;
    public Sprite emptySprite;
    public Sprite player1Sprite;
    public Sprite player2Sprite;
    public Image buttonImage;
    #endregion

    #region private properties
    private GameState gameState;
    #endregion


    #region event broadcasting
    // letting other objects know a player has taken a turn and which 
    // position they chose
    public delegate void TurnTaken(int position);
    public static event TurnTaken OnTurnTaken; 
    #endregion

    #region IPointerClickHandler methods
    public void OnPointerClick(PointerEventData eventData)
    {
        switch (gameState)
        {
            case GameState.DRAW:
                break;
            case GameState.PLAYER_1_WINS:
                break;
            case GameState.PLAYER_2_WINS:
                break;
            case GameState.PLAYER_1_TURN:
                buttonImage.sprite = player1Sprite;
                OnTurnTaken?.Invoke(boardPosition);
                break;
            case GameState.PLAYER_2_TURN:
                buttonImage.sprite = player2Sprite;
                OnTurnTaken?.Invoke(boardPosition);
                break;
        }        
    } 
    #endregion

    #region MonoBehaviour Methods
    private void Start()
    {
        ResetButton.OnResetClicked += ResetGame;
        BoardEngine.OnTurnEnded += UpdateGameState;
        ResetGame();
    }
    private void Update()
    {


    }
    #endregion

    #region private methods
    private void UpdateGameState(GameState state)
    {
        gameState = state;
    }
    private void ResetGame()
    {
        buttonImage.sprite = emptySprite;
        gameState = GameState.PLAYER_1_TURN;
    } 
    #endregion

}
