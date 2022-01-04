using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Board m_Board;
    public Piece m_Player;

    private bool m_PlayerTurn;
    public bool isPlayerTurn { get { return m_PlayerTurn; } set { m_PlayerTurn = value; } }

    // Start is called before the first frame update
    void Start()
    {
        m_PlayerTurn = false;    
    }

    // Update is called once per frame
    void Update()
    {
        if (!m_Player.isMoving)
        {
            m_PlayerTurn=true;
        }
    }
}
