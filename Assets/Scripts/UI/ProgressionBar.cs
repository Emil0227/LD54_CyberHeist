using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressionBar : MonoBehaviour
{
    [SerializeField] private GameObject m_Player;
    [SerializeField] private GameObject m_Exit;

    private float m_PlayerInitialPositionY;
    private float m_LevelLength;
    private float m_LevelProgress;
    private Vector3 m_VFullProgress;

    void Start()
    {
        m_PlayerInitialPositionY = m_Player.transform.position.y;
        m_LevelLength = m_Exit.transform.position.y - m_PlayerInitialPositionY;
        m_VFullProgress = gameObject.transform.localScale;
    }

    void Update()
    {
        m_LevelProgress = (m_Player.transform.position.y - m_PlayerInitialPositionY) / m_LevelLength;
        gameObject.transform.localScale = new Vector3(m_VFullProgress.x * m_LevelProgress, 
            m_VFullProgress.y, m_VFullProgress.z);
    }
}
