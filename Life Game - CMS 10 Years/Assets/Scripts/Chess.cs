using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using   UnityEngine.UI;

public class Chess : MonoBehaviour
{

    public Image ChessImage;
    public Button ChessButton;

    public Sprite Black;
    public Sprite White;
    public Vector2Int Coordinate;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public enum State   //枚举值，作为储存棋子的状态的值。
    {
        Blank,
        White,
        Black
    }

    public void ChangeState(State state)
    {       //改变棋子的状态，并改变其可操作性和显示的图片。
        if (state == State.Blank)
        {
            ChessButton.interactable = true;
            ChessImage.color = new Color(1, 1, 1, 0);
        }
        else if (state == State.White)
        {
            ChessButton.interactable = false;
            ChessImage.sprite = White;
            ChessImage.color = new Color(1, 1, 1, 1);
        }
        else if (state == State.Black)
        {
            ChessButton.interactable = false;
            ChessImage.sprite = Black;
            ChessImage.color = new Color(1, 1, 1, 1);
        }
    }

    public void OnClick()
    {
        if(GameManager.Instance.GameOver==false){
            if(GameManager.Instance.ChessBoardVer==1)
            {
                GameManager.Instance.PutChess(Coordinate);
            }
            else
            GameManager.Instance.ChessBoardTurn(Coordinate);
        }
    }

}   
 
    



 

