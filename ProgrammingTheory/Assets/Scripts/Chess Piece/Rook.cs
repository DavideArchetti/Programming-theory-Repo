using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rook : Piece               // INHERITANCE
{
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Rook START");
        m_MaxSteps = 2;
        Init();
    }

    // Update is called once per frame
    void Update()
    {
        ThinkMove();
        UpdateMove();
    }

    protected override void ThinkMove()                 // POLYMORPHISM
    {
        if (m_GameManager.isAITurn)
        {
            if (!isMoving && m_StepsToDo.Count == 0 && !isMoveDone)
            {
                Vector3 playerDirection = m_GameManager.GetDirectionToPlayer(transform.position);
                Vector3 direction;

                if (Mathf.Abs(playerDirection.x) > Mathf.Abs(playerDirection.z))
                {
                    //Move(new Vector3(Mathf.Sign(playerDirection.x), 0, 0));
                    direction = new Vector3(Mathf.Sign(playerDirection.x), 0, 0);
                }
                else
                {
                    //Move(new Vector3(0,0,Mathf.Sign(playerDirection.z)));
                    direction = new Vector3(0,0,Mathf.Sign(playerDirection.z));
                }

                Move(direction);

                //Vector3 nextPosition = m_BoardPosition;
                //for (int s = 0; s < m_MaxSteps; s++)
                //{
                //    nextPosition += (int)m_GameManager.m_Board.m_SquareDistance * direction;
                //    m_StepsToDo.Enqueue(nextPosition);
                //}

                if (m_StepsToDo.Count < m_MaxSteps)
                {
                    //Full Movement not found!!!!
                    //But for this piece is still valid!
                    if (m_StepsToDo.Count == 0)
                    {
                        //Movement not found!!!!
                        Debug.Log("MOVEMENT NOT FOUND!");
                        m_MoveDone = true;
                    }
                }

            }
        }
    }
}
