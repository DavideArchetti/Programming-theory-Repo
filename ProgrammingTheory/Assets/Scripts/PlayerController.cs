using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Piece
{

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("PlayerController start");
        m_GameManager = GameObject.FindWithTag("GameController").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (m_GameManager.isPlayerTurn)
        {
            float horizontal = Input.GetAxis("Horizontal");
            bool goRight  = Input.GetKeyDown(KeyCode.RightArrow);
            bool goLeft = Input.GetKeyDown(KeyCode.LeftArrow);
            bool goForward = Input.GetKeyDown(KeyCode.UpArrow);
            bool goBack = Input.GetKeyDown(KeyCode.DownArrow);

            if (goRight && transform.position.x < (m_GameManager.m_Board.m_Dimension.x-1) * m_GameManager.m_Board.m_SquareDistance)
            {
                Debug.Log("Going Right");
                Move(Vector3.right);
                m_GameManager.isPlayerTurn = false;
            }
            if (goLeft && transform.position.x > 0)
            {
                Move(Vector3.left);
                m_GameManager.isPlayerTurn = false;
            }
            if (goForward && transform.position.z < (m_GameManager.m_Board.m_Dimension.z - 1) * m_GameManager.m_Board.m_SquareDistance)
            {
                Move(Vector3.forward);
                m_GameManager.isPlayerTurn = false;
            }
            if (goBack && transform.position.z > 0)
            {
                Move(Vector3.back);
                m_GameManager.isPlayerTurn = false;
            }
        }

        UpdateMove();
    }
}
