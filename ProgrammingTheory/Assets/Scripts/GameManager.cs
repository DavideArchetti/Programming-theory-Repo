using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI movesText;
    public GameObject gameOverUI;

    public Board m_Board;
    public Piece m_Player;
    public List<Piece> m_AIPieces;

    private bool m_GameOver;
    public bool IsGameOver => m_GameOver;
    private int m_MoveCount;

    private bool m_PlayerTurn;
    public bool isPlayerTurn { get { return m_PlayerTurn; } set { m_PlayerTurn = value; } }             // ENCAPSULATION

    private bool m_AITurn;
    public bool isAITurn { get { return m_AITurn; } set { if (m_AITurn != value) { m_AITurn = value;} } }           // ENCAPSULATION

    // Start is called before the first frame update
    void Start()
    {
        m_PlayerTurn = false;    
        m_AITurn=false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!m_PlayerTurn && !m_AITurn)
        {
            m_PlayerTurn=true;
        }
        else
        if (m_PlayerTurn && m_Player.isMoveDone)
        {
            m_PlayerTurn = false;
            m_AITurn = true;
        }
        else
        if (m_AITurn && m_AIPieces.All(x => x.isMoveDone))
        {
            Debug.Log("RESET TURN");
            m_AIPieces.ForEach(x => x.ResetMove());
            m_Player.ResetMove();

            m_PlayerTurn = false;
            m_AITurn = false;
        }

    }

    public Vector3 GetDirectionToPlayer(Vector3 origin)
    {
        return (m_Player.transform.position - origin).normalized;
    }

    public bool IsPositionValid(Vector3 pos)
    {
        if (pos.x < 0 || pos.z < 0) return false;
        if (pos.x > (m_Board.m_Dimension.x - 1) * m_Board.m_SquareDistance) return false;
        if (pos.z > (m_Board.m_Dimension.z - 1) * m_Board.m_SquareDistance) return false;

        return true;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="pos"></param>
    /// <returns>true if the position pos won't be already taken by a piece</returns>
    public bool IsPositionFree(Vector3 pos)
    {
        return !m_AIPieces.Any(x => x.IsPositionFinal(pos));
    }

    public void IncreaseMoveCount()
    {
        m_MoveCount++;
        movesText.text = $"Moves: {m_MoveCount.ToString()}";
    }

    public void GameOver()
    {
        m_GameOver = true;
        gameOverUI.gameObject.SetActive(true);
    }

    public void PlayAgain()
    {
        m_MoveCount = 0;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
