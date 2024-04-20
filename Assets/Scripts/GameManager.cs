using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.UI;
using UnityEditor;

public class GameManager : MonoBehaviour
{
    public Text TextScore;

    private void Awake()
    {
        GameState.IsGamePaused = false;
        GameState.Score = 0;
    }
    void Update()
    {
        TextScore.text = "Score: " + GameState.Score.ToString();
    }
}
