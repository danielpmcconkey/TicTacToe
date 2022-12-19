using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ExitButton : MonoBehaviour, IPointerClickHandler
{
    #region properties exposed to the inspector
    
    #endregion

    

    #region IPointerClickHandler Methods
    public void OnPointerClick(PointerEventData eventData)
    {
        Application.Quit(); // only works when game is built
        UnityEditor.EditorApplication.isPlaying = false; // works in Unity editor
    }
    #endregion

    #region MonoBehaviour Methods
    void Start()
    {
        BoardEngine.OnTurnEnded += CheckForGameOver;
        ResetButton.OnResetClicked += ResetGame;
        Hide();
    }
    void Update()
    {

    }
    #endregion

    #region private methods
    private void CheckForGameOver(GameState gameState)
    {
        switch (gameState)
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
    private void ResetGame()
    {
        Hide();
    }
    #endregion


}

