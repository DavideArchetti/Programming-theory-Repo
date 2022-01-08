using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Horse : Piece                  // INHERITANCE
{
    void Start()
    {
        Debug.Log("Horse START");
        m_MaxSteps = 2;
        Init();
    }

    // Update is called once per frame
    void Update()
    {
        ThinkMove();
        UpdateMove();
    }

    protected override void ThinkMove()             // POLYMORPHISM
    {
        if (m_GameManager.isAITurn)
        {
            if (!isMoving && m_StepsToDo.Count == 0 && !isMoveDone)
            {
                Vector3 playerDirection = m_GameManager.GetDirectionToPlayer(transform.position);

                if (Mathf.Abs(playerDirection.x) > Mathf.Abs(playerDirection.z))
                {
                    Vector3 nextPosition = m_BoardPosition + new Vector3(Mathf.Sign(playerDirection.x) * m_GameManager.m_Board.m_SquareDistance,0,0);
                    if (m_GameManager.IsPositionValid(nextPosition) && m_GameManager.IsPositionFree(nextPosition))
                        m_StepsToDo.Add(nextPosition);

                    nextPosition += new Vector3(Mathf.Sign(playerDirection.x) * m_GameManager.m_Board.m_SquareDistance,0,Mathf.Sign(playerDirection.z) * m_GameManager.m_Board.m_SquareDistance );

                    if (m_GameManager.IsPositionValid(nextPosition) && m_GameManager.IsPositionFree(nextPosition))
                        m_StepsToDo.Add(nextPosition);
                }
                else
                {
                    Vector3 nextPosition = m_BoardPosition + new Vector3(0,0,Mathf.Sign(playerDirection.z) * m_GameManager.m_Board.m_SquareDistance);
                    if (m_GameManager.IsPositionValid(nextPosition) && m_GameManager.IsPositionFree(nextPosition))
                        m_StepsToDo.Add(nextPosition);

                    nextPosition += new Vector3(Mathf.Sign(playerDirection.x) * m_GameManager.m_Board.m_SquareDistance, 0, Mathf.Sign(playerDirection.z) * m_GameManager.m_Board.m_SquareDistance);

                    if (m_GameManager.IsPositionValid(nextPosition) && m_GameManager.IsPositionFree(nextPosition))
                        m_StepsToDo.Add(nextPosition);
                }


                if (m_StepsToDo.Count < m_MaxSteps)
                {
                    //Full Movement not found!!!!

                    Debug.Log("MOVEMENT NOT FOUND!");
                    m_StepsToDo.Clear();
                    m_MoveDone = true;

                }
            }
        }
    }

}
