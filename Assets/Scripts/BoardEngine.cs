using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Assets.Scripts;

public class BoardEngine : MonoBehaviour
{
    /*
     * The TTT board is represented by an array of 9 ints. The array 
     * indicies map to the TTT positions as such:
     * 
     *          0 | 1 | 2
     *         ---+---+---
     *          3 | 4 | 5
     *         ---+---+---
     *          6 | 7 | 8
     * 
     * Position "ownership" is represented by the value stored in the array
     * at each position. Each player is assigned a multiplier, with player
     * 1 having a positive multiplier and player 2 having a negative 
     * multiplier. Thus, in the board below, player 1 owns the middle 
     * square, player 2 owns the lower right square, and all others are 
     * open.
     * 
     *          0 | 0 | 0
     *         ---+---+---
     *          0 | 1 | 0
     *         ---+---+---
     *          0 | 0 | -1
     * 
     * Win conditions can therefore easily be determined. If the sum of any
     * row, column, or diagonal is 3 times either player's multiplier, that
     * has won the game.  
     *
     * */

    #region properties exposed to the inspector
    #endregion

    #region private and read-only properties
    private int[] squares;
    private int[][] winPaths;
    const int player1Multiplier = 1;
    const int player2Multiplier = -1;
    public int activePlayer { get; private set; }
    public GameState gameState { get; private set; }
    #endregion


    // set up the turn ended event to broadcast every EndTurn()
    public delegate void TurnEnded();
    public static event TurnEnded OnTurnEnded;

    #region MonoBehaviour Methods
    void Start()
    {
        ResetButton.OnResetClicked += ResetBoard;
        winPaths = new int[][]
        {
            new int[] { 0, 1, 2 },
            new int[] { 3, 4, 5 },
            new int[] { 6, 7, 8 },
            new int[] { 0, 3, 6 },
            new int[] { 1, 4, 7 },
            new int[] { 2, 5, 8 },
            new int[] { 0, 4, 8 },
            new int[] { 2, 4, 6 }
        };

        ResetBoard();
    }
    void Update()
    {

    }
    #endregion

    #region public methods
    public void PlaceToken(int position)
    {
        if (gameState == GameState.DRAW || gameState == GameState.PLAYER_1_WINS || gameState == GameState.PLAYER_2_WINS)
            return; // Game's over, yo!
        // check if position is already occupied
        if (squares[position] != 0) return; // Illegal move
        // assign the square
        squares[position] = (activePlayer == 1) ? player1Multiplier : player2Multiplier;
        // now scurry!
        EndTurn();
    }
    #endregion

    #region private methods
    private void EndTurn()
    {
        (bool isWin, int player) winState = IsWin();
        if(winState.isWin)
        {
            gameState = winState.player == 1 ? GameState.PLAYER_1_WINS : GameState.PLAYER_2_WINS;
        }
        else if (IsDrawn()) gameState = GameState.DRAW;
        else if(activePlayer == 1)
        {
            gameState = GameState.PLAYER_2_TURN;
            activePlayer = 2;
        }
        else
        {
            gameState = GameState.PLAYER_1_TURN;
            activePlayer = 1;
        }

        OnTurnEnded?.Invoke();
    }
    private bool IsDrawn()
    {
        for (int i = 0; i < squares.Length; i++)
        {
            if (squares[i] == 0) return false;
        }
        return true;
    }
    private (bool, int) IsWin()
    {
        foreach (var winPath in winPaths)
        {
            int sumPath = squares[winPath[0]] + squares[winPath[1]] + squares[winPath[2]];
            if (sumPath == 3 * player1Multiplier) return (true, 1);
            if (sumPath == 3 * player2Multiplier) return (true, 2);
        }
        return (false, 0);
    }
    private void ResetBoard()
    {
        squares = new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0 };
        activePlayer = 1;
        gameState = GameState.PLAYER_1_TURN;
    }
    #endregion
}
