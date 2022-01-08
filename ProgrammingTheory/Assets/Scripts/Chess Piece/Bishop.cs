using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bishop : Piece                         // INHERITANCE
{
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Bishop START");
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
                Vector3 direction;

                direction = new Vector3(Mathf.Sign(playerDirection.x), 0, Mathf.Sign(playerDirection.z));

                Move(direction);

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
