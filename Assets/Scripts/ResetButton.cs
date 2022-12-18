using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ResetButton : MonoBehaviour, IPointerClickHandler
{
    #region properties exposed to the inspector
    public BoardEngine board;
    #endregion

    // set up the reset event to broadcast every OnPointerClick()
    public delegate void ResetClicked();
    public static event ResetClicked OnResetClicked;

    #region IPointerClickHandler Methods
    public void OnPointerClick(PointerEventData eventData)
    {
        OnResetClicked?.Invoke();        
        Hide();
    }
    #endregion

    #region MonoBehaviour Methods
    void Start()
    {
        BoardEngine.OnTurnEnded += CheckForGameOver;
        Hide();
    }
    void Update()
    {
        
    }
    #endregion

    #region private methods
    private void CheckForGameOver()
    {
        switch (board.gameState)
        {
            case GameState.PLAYER_1_WINS:
                Display();
                break;
            case GameState.PLAYER_2_WINS:
                Display();
                break;
            case GameState.DRAW:
                Display();
                break;
            default:
                break;

        }
    }
    private void Display()
    {
        gameObject.SetActive(true);
    }
    private void Hide()
    {
        gameObject.SetActive(false);
    }
    #endregion


}
