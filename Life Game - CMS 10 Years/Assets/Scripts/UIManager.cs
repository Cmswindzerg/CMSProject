using UnityEngine;
using UnityEngine.UI;


public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }
    public GameObject GameResult;
    public Text ResultMessage;
    public Text BlackScore;
    public Text WhiteScore;
    public Text BlackID;
    public Text WhiteID;
    public Transform ChessParent;
    public Chess ChessPrefab;
    private Chess[,] _chesses = new  Chess[8,8];
    public Transform ChessReferencePoint;
    private int _chessBoardVer = 0;
  
    // Start is called before the first frame update
    void Start()
    {
        CreateChesses();
        RefreshChesses();
    }

    // Update is called once per frame
    void Update()
    {
        /*if (Input.GetKeyDown(KeyCode.Space))
        {
            ShowMessage("msg");
        }*/
        if (_chessBoardVer != GameManager.Instance.ChessBoardVer)
        {
            _chessBoardVer = GameManager.Instance.ChessBoardVer;
            RefreshChesses();
        }
        if(GameManager.Instance.GameOver){
            if(GameManager.Instance.BlackWin)
            ShowMessage("游戏结束，黑方胜！");
            if(GameManager.Instance.WhiteWin)
            ShowMessage("游戏结束，白方胜！");
            if(GameManager.Instance.BothWin)
            ShowMessage("游戏结束，平局！");
        }

    }
    public void SetMessageBoxActive(bool active)
    {
        GameResult.SetActive(active);

    }

    public void SetScore(bool setBlack, int score)
    {
        if (setBlack)
        {
            BlackScore.text = score.ToString();
        }
        else
        {
            WhiteScore.text = score.ToString();
        }
    }

    public void ShowMessage(string message)
    {
        ResultMessage.text = message;
        SetMessageBoxActive(true);
    }

    public void CreateChesses() {
        for (int x = 0; x < 8; x++) {
            for (int y = 0; y < 8; y++) {
                _chesses[x, y] = Instantiate<Chess>(ChessPrefab, ChessReferencePoint.position + new Vector3(x * 70,y * 70), Quaternion.identity, ChessParent);
                _chesses[x, y].Coordinate = new Vector2Int(x, y);
            }
        } 
    }

    public void RefreshChesses()
    {
        for(int x = 0; x < 8; x++)
        {
            for(int y = 0; y < 8; y++)
            {
                _chesses[x, y].ChangeState(GameManager.Instance.ChessBoard[x, y]);
            }
        }
        BlackScore.text = GameManager.Instance.BlackScore.ToString();
        WhiteScore.text = GameManager.Instance.WhiteScore.ToString();
    }

    public void RestartGame(){
        GameManager.Instance.InitGame();
        SetMessageBoxActive (false);
    }
  
    }



