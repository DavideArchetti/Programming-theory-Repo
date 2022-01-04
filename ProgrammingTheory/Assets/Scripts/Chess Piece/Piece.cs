using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Piece : MonoBehaviour
{
    private const float m_Tolerance = 0.05f;  //Our precision for position movement
    protected int m_MaxSteps = 1;       //max number of steps a piece can do
    protected Vector3 m_BoardPosition;  //Position on the checkboard (0,0,0) is the bottom left square of the board, Y is up not used
    protected Vector3 m_NextPosition;
    protected float m_MovementSpeed = 5.0f;

    protected GameManager m_GameManager;

    public bool isMoving => m_BoardPosition != m_NextPosition; 

    private void Start()
    {
        Debug.Log("Piece start");
  
    }

    virtual protected void UpdateMove()
    {
        Debug.Log("Piece Update");
        if (m_NextPosition != m_BoardPosition)
        {
            Vector3 direction = (m_NextPosition - m_BoardPosition).normalized;
            Debug.Log("direction:" + direction);
            transform.Translate(direction * Time.deltaTime * m_MovementSpeed);
            if ((transform.position - m_NextPosition).magnitude < m_Tolerance)
            {
                transform.position = m_NextPosition;
                m_BoardPosition = m_NextPosition;
            }
        }
    }

    virtual public void Move(Vector3 direction)
    {
        Debug.Log("Piece Move direction:" + direction + " position:" + m_BoardPosition);
        m_NextPosition = m_BoardPosition + (int)m_GameManager.m_Board.m_SquareDistance * direction;
        Debug.Log("next position:" + m_NextPosition);
    }

}
