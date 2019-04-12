﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public float m_fTimer;
    private bool m_bActive;
    public Text m_tCountdownTxt;

    public PlayerController[] m_goPlayers;
    
	void Start ()
    {
        m_bActive = false;
        m_fTimer = 0;

        m_tCountdownTxt.enabled = false;

        for (int i = 0; i < m_goPlayers.Length; i++)
        {
            m_goPlayers[i].GetComponent<PlayerController>().m_bActive = false;
        }
    }
	
	void Update ()
    {
        m_fTimer += Time.deltaTime;
        if(m_bActive == false)
        {
            CountDown();
        }
	}

    public void CountDown()
    {
        if(m_fTimer >= 1)
        {
            m_tCountdownTxt.text = "3";
            m_tCountdownTxt.enabled = true;

            if (m_fTimer >= 2)
            {
                m_tCountdownTxt.text = "2";

                if (m_fTimer >= 3)
                {
                    m_tCountdownTxt.text = "1";

                    if (m_fTimer >= 4)
                    {
                        m_tCountdownTxt.text = "Go!";
                        for (int i = 0; i < m_goPlayers.Length; i++)
                        {
                            m_goPlayers[i].GetComponent<PlayerController>().m_bActive = true;
                        }

                        if (m_fTimer >= 5)
                        {
                            m_tCountdownTxt.enabled = false;
                            m_bActive = true;
                        }
                    }
                }
            }
        }
    }
}