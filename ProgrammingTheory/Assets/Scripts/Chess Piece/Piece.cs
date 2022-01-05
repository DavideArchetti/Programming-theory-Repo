using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class Piece : MonoBehaviour
{
    private const float m_Tolerance = 0.05f;        //Our precision for position movement
    protected int m_MaxSteps = 1;                   //max number of steps a piece can do
    protected Vector3 m_BoardPosition;              //Position on the checkboard (0,0,0) is the bottom left square of the board, Y is up not used
    protected Vector3 m_NextPosition;
    protected float m_MovementSpeed = 5.0f;
    protected List<Vector3> m_StepsToDo = new List<Vector3>();

    protected GameManager m_GameManager;

    public bool isMoving => m_BoardPosition != m_NextPosition; 
    private bool m_MoveDone = false;
    public bool isMoveDone => m_MoveDone;

    private void Start()
    {
        Debug.Log("Piece start");
  
    }

    virtual protected void UpdateMove()
    {
        if (isMoving)
        {
            Vector3 direction = (m_NextPosition - m_BoardPosition).normalized;

            transform.Translate(direction * Time.deltaTime * m_MovementSpeed);
            if ((transform.position - m_NextPosition).magnitude < m_Tolerance)
            {
                transform.position = m_NextPosition;
                m_BoardPosition = m_NextPosition;

                if (m_StepsToDo.Count == 0)
                {
                    m_MoveDone = true;
                }
            }
        }
        else
        {
            m_MoveDone = false;
            if (m_StepsToDo.Count > 0)
            {
                m_NextPosition = m_StepsToDo[0];
                m_StepsToDo.RemoveAt(0);
            }
        }
    }

    /// <summary>
    /// Move a piece in the direction for m_MaxSteps allowes
    /// </summary>
    /// <param name="direction"></param>
    virtual public void Move(Vector3 direction)
    {
        Vector3 nextPosition = m_BoardPosition;
        for (int s = 0; s < m_MaxSteps; s++)
        {
            nextPosition += (int)m_GameManager.m_Board.m_SquareDistance * direction;
            if (m_GameManager.IsPositionValid(nextPosition) && m_GameManager.IsPositionFree(nextPosition))
                m_StepsToDo.Add(nextPosition);
        }
    }

    protected void Init()
    {
        m_GameManager = GameObject.FindWithTag("GameController").GetComponent<GameManager>();
        m_NextPosition = m_BoardPosition = transform.position;
    }


    /// <summary>
    /// Check if the position pos will the the position that this piece will reach at the end of the movement
    /// </summary>
    /// <param name="pos"></param>
    /// <returns></returns>
    public bool IsPositionFinal(Vector3 pos)
    {
        if (m_StepsToDo.Count()> 0)
            return (pos - m_StepsToDo.Last()).magnitude < m_Tolerance;
        return false;
    }

}
