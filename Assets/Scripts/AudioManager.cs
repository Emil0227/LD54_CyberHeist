using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private AudioSource[] m_ArrayMusic;
    private AudioSource m_Music1;
    private AudioSource m_Music2;

    void Start()
    {
        m_ArrayMusic = gameObject.GetComponents<AudioSource>();
        m_Music1 = m_ArrayMusic[0];
        m_Music2 = m_ArrayMusic[1];
    }

    void Update()
    {
        if (GameState.IsGamePaused)
        {
            m_Music1.Stop();
            m_Music2.Stop();
        }
    }
}
