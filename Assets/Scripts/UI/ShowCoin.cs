using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.UI;
using UnityEditor;

public class ShowCoin : MonoBehaviour
{
    [SerializeField] private Text m_TextScore;

    void Start()
    {
        m_TextScore.text = "Score: " + GameState.Score.ToString();
    }
}
