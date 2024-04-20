using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Text m_TextScore;
    [SerializeField] private GameObject m_BreakerReadyPrompt;
    [SerializeField] private GameObject m_Bullet;
    [SerializeField] private GameObject m_CDmask;
    [SerializeField] private GameObject m_Player;

    private Timer m_CDTimer;
    private bool m_IsGameStart;
    private float m_BreakerCD;

    void Start()
    {
        GameState.IsGamePaused = false;
        GameState.Score = 0;
        m_CDmask.GetComponent<Image>().fillAmount = 0;
        m_CDTimer = gameObject.AddComponent<Timer>();
        m_IsGameStart = true;
        m_BreakerCD = 3;
    }

    void Update()
    {
        m_TextScore.text = "Score: " + GameState.Score.ToString();
        if (m_IsGameStart || m_CDTimer.Finished)//ready to shoot breaker
        {
            Shooting();
        }
        else
        {
            m_BreakerReadyPrompt.SetActive(false);
        }
    }

    void Shooting()
    {
        m_BreakerReadyPrompt.SetActive(true);
        if (Input.GetKeyDown(KeyCode.Space))//shoot
        {
            m_IsGameStart = false;
            //reset timer
            m_CDTimer.Duration = m_BreakerCD;
            m_CDTimer.Run();
            //set countdown icon
            m_BreakerReadyPrompt.SetActive(false);
            m_CDmask.GetComponent<Image>().fillAmount = 1;
            //release bullet
            GameObject bullet = GameObject.Instantiate(m_Bullet);
            bullet.transform.position = m_Player.transform.position;
        }
    }
}
