using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private int boardHeight;
    [SerializeField]
    private int boardWidth;
    [SerializeField]
    private GameObject[] gamePieces;
    [SerializeField]
    private TextMeshProUGUI scoreTextMeshPro;
    private int _score = 1000;

    private GameObject _board;
    private GameObject[,] _gameBoard;
    private Vector3 _offset = new Vector3(0, 0, -1);
    private List<GameObject> _matchLines;
    private AudioManager audioManager;

    void Start()
    {
        _board = GameObject.Find("GameBoard");
        _gameBoard = new GameObject[boardHeight, boardWidth];
        _matchLines = new List<GameObject>();
        audioManager = AudioManager.GetAudioManager();
    }

    public void Spin()
    {
        audioManager.PlayClickSound();

        for (int i = 0; i < 3; i++)
        {
            GameObject objectToDestroy = GameObject.Find("Rows");
            if (objectToDestroy != null)
            {
                Destroy(objectToDestroy);
            }
        }

        foreach (GameObject l in _matchLines)
        {
            GameObject.Destroy(l);
        }

        _matchLines.Clear();

        _score -= 100;

        for (int i = 0; i < boardHeight; i++)
        {
            for (int j = 0; j < boardWidth; j++)
            {
                GameObject gridPosition = _board.transform.Find(i + " " + j).gameObject;
                if (gridPosition.transform.childCount > 0)
                {
                    GameObject destroyPiece = gridPosition.transform.GetChild(0).gameObject;
                    Destroy(destroyPiece);
                }
                GameObject pieceType = gamePieces[Random.Range(0, gamePieces.Length)];
                GameObject thisPiece = Instantiate(pieceType, gridPosition.transform.position + _offset, Quaternion.identity);
                thisPiece.name = pieceType.name;
                thisPiece.transform.parent = gridPosition.transform;
                _gameBoard[i, j] = thisPiece;
            }
        }
        CheckForMatches();
    }

    private void CheckForMatches()
    {
        for (int i = 0; i < boardHeight; i++)
        {
            int matchLength = 1;
            GameObject matchBegin = _gameBoard[i, 0];
            GameObject matchEnd = null;

            for (int j = 0; j < boardWidth - 1; j++)
            {
                if (_gameBoard[i, j].name == _gameBoard[i, j + 1].name)
                {
                    matchLength++;

                    if (matchLength >= 2)
                    {
                        matchEnd = _gameBoard[i, j + 1];

                        audioManager.PlayWinSound();

                        int matchPoints = CalculatePoints(_gameBoard[i, j], matchLength);

                        _score += matchPoints;

                        DrawLine(matchBegin.transform.position + _offset, matchEnd.transform.position + _offset);
                    }
                }
                else
                {
                    matchBegin = _gameBoard[i, j + 1];

                    matchLength = 1;
                }
            }
        }
        scoreTextMeshPro.text = "Score: " + _score.ToString();
    }

    private int CalculatePoints(GameObject symbolObject, int matchCount)
    {
        string symbolName = symbolObject.name;

        switch (symbolName)
        {
            case "bell":
                return (matchCount == 2) ? 150 : (matchCount == 3) ? 200 : 0;
            case "lemon":
                return (matchCount == 2) ? 250 : (matchCount == 3) ? 300 : 0;
            case "cherries":
                return (matchCount == 2) ? 350 : (matchCount == 3) ? 400 : 0;
            case "melon":
                return (matchCount == 2) ? 450 : (matchCount == 3) ? 500 : 0;
            case "heart":
                return (matchCount == 2) ? 550 : (matchCount == 3) ? 600 : 0;
            case "clover":
                return (matchCount == 2) ? 650 : (matchCount == 3) ? 700 : 0;
            case "horseshoe":
                return (matchCount == 2) ? 750 : (matchCount == 3) ? 800 : 0;
            case "Lucky7_rainbow":
                return (matchCount == 2) ? 877 : (matchCount == 3) ? 7877 : 0;
            default:
                return 0;
        }
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void Restart()
    {
        SceneManager.LoadScene(0);
    }

    private void DrawLine(Vector3 start, Vector3 end)
    {
        GameObject myLine = new GameObject();
        myLine.transform.position = start;
        myLine.AddComponent<LineRenderer>();
        LineRenderer lr = myLine.GetComponent<LineRenderer>();
        lr.material.color = Color.black;
        lr.startWidth = .05f;
        lr.endWidth = .05f;
        lr.SetPosition(0, start);
        lr.SetPosition(1, end);
        _matchLines.Add(myLine);
    }
}