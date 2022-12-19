using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VictoryLine : MonoBehaviour
{
    #region properties exposed to the inspector
    public UnityEngine.UI.Button centerSquare;
    public float overflow = 25; // the amount of space beyond the square you want the line to go 
    #endregion

    private LineRenderer line;
    private Vector2 centerPosition;
    private float buttonWidth;
    private float buttonHeight;

    #region MonoBehaviour methods
    private void Start()
    {
        line = gameObject.GetComponent<LineRenderer>();
        centerPosition = centerSquare.transform.position;
        var buttonRt = centerSquare.GetComponent<RectTransform>();
        buttonWidth = buttonRt.sizeDelta.x;
        buttonHeight = buttonRt.sizeDelta.y;
        BoardEngine.OnWinPath += SetLine;
        ResetButton.OnResetClicked += Hide;
        Hide();
    }
    private void Update()
    {

    }
    #endregion

    #region private methods
    private void Display()
    {
        gameObject.SetActive(true);
    }
    private void Hide()
    {
        gameObject.SetActive(false);
    }
    private void SetLine(int beginPosition, int endPosition)
    {
        // set up your coordinates so you can change them depending on the line orientation
        float xStart = 0f;
        float xEnd = 0f;
        float yStart = 0f;
        float yEnd = 0f;

        int rowStart = beginPosition / 3;
        int rowEnd = endPosition / 3;
        int columnStart = beginPosition % 3;
        int columnEnd = endPosition % 3;

        if(rowStart == rowEnd)
        {
            // horizontal
            xStart = centerPosition.x - (buttonWidth * 1.5f) - overflow;
            xEnd = centerPosition.x + (buttonWidth * 1.5f) + overflow;
            yStart = centerPosition.y + (buttonHeight * 1f) - (rowStart * buttonHeight);
            yEnd = centerPosition.y + (buttonHeight * 1f) - (rowEnd * buttonHeight);
        }
        else if(columnStart == columnEnd)
        {
            // vertical
            xStart = centerPosition.x - (buttonWidth * 1f) + (columnStart * buttonWidth);
            xEnd = centerPosition.x - (buttonWidth * 1f) + (columnEnd * buttonWidth);
            yStart = centerPosition.y + (buttonHeight * 1.5f) + overflow;
            yEnd = centerPosition.y - (buttonHeight * 1.5f) - overflow;
        }
        else
        {
            // diagonal
            xStart = centerPosition.x - (buttonWidth * 1.5f) - overflow;
            xEnd = centerPosition.x + (buttonWidth * 1.5f) + overflow;
            yStart = (rowStart == 0) ?
                centerPosition.y + (buttonHeight * 1.5f) + overflow :
                centerPosition.y - (buttonHeight * 1.5f) - overflow;
            yEnd = (rowEnd == 0) ?
                centerPosition.y + (buttonHeight * 1.5f) + overflow :
                centerPosition.y - (buttonHeight * 1.5f) - overflow;
        }

        
        line.SetPosition(0, new Vector2(xStart, yStart));
        line.SetPosition(1, new Vector2(xEnd, yEnd));
        Display();
    }
    #endregion
}
