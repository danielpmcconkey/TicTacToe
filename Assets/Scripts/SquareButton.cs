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
    public BoardEngine boardEngine;
    public Image buttonImage;
    #endregion

    #region IPointerClickHandler methods
    public void OnPointerClick(PointerEventData eventData)
    {
        try
        {
            int currentPlayerTurn = boardEngine.activePlayer;
            boardEngine.PlaceToken(boardPosition);
            if (currentPlayerTurn == 1) buttonImage.sprite = player1Sprite;
            else buttonImage.sprite = player2Sprite;
        }
        catch (System.ArgumentException)
        {
            // square was already owned. swallow
        }
    } 
    #endregion

    #region MonoBehaviour Methods
    private void Start()
    {
        ResetButton.OnResetClicked += ResetSprite;
        ResetSprite();
    }
    private void Update()
    {


    }
    #endregion

    #region private methods
    private void ResetSprite()
    {
        buttonImage.sprite = emptySprite;
    } 
    #endregion

}
