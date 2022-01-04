using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class King : Piece
{
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("King Start");
        m_GameManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("boardposition:" + m_BoardPosition + " nextPosition:" + m_NextPosition);
        Debug.Log("King update");

        UpdateMove();
        
    }
}
