using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public Chess.State[,] ChessBoard = new Chess.State[8, 8];
    public Chess.State[,] TempChessBoard = new Chess.State[8, 8];
    public int ChessBoardVer  {get; private set; }
    public bool _isBlackTurn;
    public int BlackScore {get; private set;}
    public int WhiteScore {get; private set;}
    public bool GameOver {get; private set;}
    public bool WhiteWin {get; private set;}
    public bool BlackWin {get; private set;}
    public bool BothWin {get; private set;}


private void Awake()
    {
        Instance = this;
        InitGame();
        
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {                 

    }
    public void InitGame()
    {
        for(int x = 0; x< 8; x++)
        {
            for(int y = 0; y < 8; y++)
            {
                ChessBoard[x, y] = Chess.State.Blank;

            }
        }
        ChessBoard[3, 4] = Chess.State.Black;
        ChessBoard[4, 4] = Chess.State.White;
        ChessBoard[4, 3] = Chess.State.Black;
        ChessBoard[3, 3] = Chess.State.White;

        GameOver =false;
        _isBlackTurn = true;
        WhiteWin=false;
        BlackWin=false;
        BothWin=false;
        ChessBoardVer = 0;
        ScoreCount();
    }

    public void TempChess()
    {
            for (int x = 0; x < 8; x++)
        {
            for(int y = 0;y < 8; y++)
            {
                int cellNumber = 0;
                int cellBlackNumber = 0;
                int cellWhiteNumber = 0;
                for (int a = -1;a < 2 ;a++)
                {
                    for(int b = -1; b < 2; b++)
                    {
                        if (x + a < 0 || x + a > 7 || y + b < 0 || y + b > 7) continue;
                        if (a == 0 && b == 0) continue;
                        if (ChessBoard[x + a, y + b] == Chess.State.Black)
                            cellBlackNumber++;
                        else if (ChessBoard[x + a, y + b] == Chess.State.White)
                            cellWhiteNumber++;
                        cellNumber = cellBlackNumber + cellWhiteNumber;
                        //Debug.Log(cellNumber);
                    }                
                }
                if (cellNumber < 2 || cellNumber > 3)
                {
                    TempChessBoard[x, y] = Chess.State.Blank;
                }
                else if (cellNumber == 3 && ChessBoard[x, y]== Chess.State.Blank)
                {
                    if (cellBlackNumber > cellWhiteNumber)
                        TempChessBoard[x, y] = Chess.State.Black;
                    else
                        TempChessBoard[x, y] = Chess.State.White;
                }
                else
                {
                    TempChessBoard[x, y] = ChessBoard[x, y];
                }
            }
        }
        for (int x = 0; x < 8; x++)
        {
            for (int y = 0; y < 8; y++)
            {
                ChessBoard[x, y] = TempChessBoard[x, y];
            }
        }
    }

    public void ChessBoardTurn(Vector2Int coordinate)
    {
        PutChess(coordinate);
        if(ChessBoardVer!=2)
            _isBlackTurn = !_isBlackTurn;
        TempChess();
        ScoreCount();
        WinTheGame();
        //Debug.Log(BlackScore);
    }

    public void PutChess(Vector2Int coordinate){
        if (_isBlackTurn == true)
            ChessBoard[coordinate.x, coordinate.y] = Chess.State.Black;
        else
            ChessBoard[coordinate.x, coordinate.y] = Chess.State.White;
        ChessBoardVer++;
    }

    public void ScoreCount(){
        BlackScore = 0;
        WhiteScore = 0;
         for (int x = 0; x < 8; x++)
        {
            for (int y = 0; y < 8; y++)
            {
                if(ChessBoard[x, y] == Chess.State.Black)
                BlackScore++;
                if(ChessBoard[x, y] == Chess.State.White)
                WhiteScore++;
            }
        }
    }

    public void WinTheGame(){
        if(ChessBoardVer == 50||WhiteScore == 0||BlackScore == 0||WhiteScore>32||BlackScore>32){
            GameOver = true;
            if(WhiteScore > BlackScore){
                WhiteWin = true;
            }
            else if(WhiteScore < BlackScore){
                BlackWin = true;
            }
            else
                BothWin = true;

        }
    }
    
}
