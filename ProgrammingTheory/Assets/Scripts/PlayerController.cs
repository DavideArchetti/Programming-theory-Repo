using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Piece           // INHERITANCE
{

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("PlayerController start");
        Init();
    }

    // Update is called once per frame
    void Update()
    {
        if (m_GameManager.IsGameOver) return;
        if (m_GameManager.isPlayerTurn && !isMoving)
        {
            bool goRight  = Input.GetKeyDown(KeyCode.RightArrow) | Input.GetKeyDown(KeyCode.Keypad6);
            bool goLeft = Input.GetKeyDown(KeyCode.LeftArrow) | Input.GetKeyDown(KeyCode.Keypad4);
            bool goForward = Input.GetKeyDown(KeyCode.UpArrow) | Input.GetKeyDown(KeyCode.Keypad8);
            bool goBack = Input.GetKeyDown(KeyCode.DownArrow) | Input.GetKeyDown(KeyCode.Keypad2);
            bool goNE = Input.GetKeyDown(KeyCode.Keypad9);
            bool goNW = Input.GetKeyDown(KeyCode.Keypad7);
            bool goSE = Input.GetKeyDown(KeyCode.Keypad3);
            bool goSW = Input.GetKeyDown(KeyCode.Keypad1);

            if (goRight && transform.position.x < (m_GameManager.m_Board.m_Dimension.x-1) * m_GameManager.m_Board.m_SquareDistance)
            {
                Move(Vector3.right);
            }
            if (goLeft && transform.position.x > 0)
            {
                Move(Vector3.left);
            }
            if (goForward && transform.position.z < (m_GameManager.m_Board.m_Dimension.z - 1) * m_GameManager.m_Board.m_SquareDistance)
            {
                Move(Vector3.forward);
            }
            if (goBack && transform.position.z > 0)
            {
                Move(Vector3.back);
            }
            if (goNE && transform.position.z < (m_GameManager.m_Board.m_Dimension.z - 1) * m_GameManager.m_Board.m_SquareDistance &&
                        transform.position.x < (m_GameManager.m_Board.m_Dimension.x - 1) * m_GameManager.m_Board.m_SquareDistance)
            {
                Move(new Vector3(1, 0, 1));
            }
            if (goNW && transform.position.z < (m_GameManager.m_Board.m_Dimension.z - 1) * m_GameManager.m_Board.m_SquareDistance &&
                        transform.position.x > 0)
            {
                Move(new Vector3(-1,0, 1));
            }
            if (goSE && transform.position.z > 0 &&
                        transform.position.x < (m_GameManager.m_Board.m_Dimension.x - 1) * m_GameManager.m_Board.m_SquareDistance)
            {
                Move(new Vector3(1, 0, -1));
            }
            if (goSW && transform.position.x > 0 &&
                        transform.position.z > 0)
            {
                Move(new Vector3(-1, 0, -1));
            }

            if (goBack || goForward || goLeft || goRight || goNE || goNW || goSE || goSW)
            {
                m_GameManager.isPlayerTurn = false;
                m_GameManager.IncreaseMoveCount();
            }
        }


        UpdateMove();
    }

    private void OnTriggerEnter(Collider other)
    {
        string otherName = other.gameObject.GetComponent<Piece>().GetType().Name;
        Debug.Log("OnTriggerEnter:" + otherName);

        if (otherName == "Horse" && other.gameObject.GetComponent<Piece>().isLastPosition)
        {
            Debug.Log("Catch!");
            m_GameManager.GameOver();
        }
        
        if (otherName != "Horse")
        {
            Debug.Log("Catched!");
            m_GameManager.GameOver();
        }
    }
}
