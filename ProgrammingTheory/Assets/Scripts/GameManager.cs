using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Board m_Board;
    public Piece m_Player;
    public List<Piece> m_AIPieces;

    private bool m_PlayerTurn;
    public bool isPlayerTurn { get { return m_PlayerTurn; } set { m_PlayerTurn = value; } }

    private bool m_AITurn;
    public bool isAITurn { get { return m_AITurn; } set { if (m_AITurn != value) { m_AITurn = value;} } }

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
}
