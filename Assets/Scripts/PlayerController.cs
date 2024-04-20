using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private GameObject m_LooseUI;
    [SerializeField] private GameObject m_WinUI;

    private Rigidbody2D m_PlayerRigidbody;
    private float m_PlayerSpeedVertical;
    private float m_PlayerSpeedHorizontal;
    private Animator m_Anim;

    

    void Start()
    {
        m_PlayerRigidbody = GetComponent<Rigidbody2D>();
        m_PlayerSpeedVertical = 5;
        m_PlayerSpeedHorizontal = 5;
        m_Anim = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        //player movement
        GetComponent<Rigidbody2D>().velocity = Vector3.zero;
        if (GameState.IsGamePaused == false)
        {
            Vector2 position = transform.position;
            position.x += Input.GetAxisRaw("Horizontal") * m_PlayerSpeedHorizontal * Time.fixedDeltaTime;
            position.y += m_PlayerSpeedVertical * Time.fixedDeltaTime;
            m_PlayerRigidbody.MovePosition(position);
        }
        else
        {
            m_Anim.SetBool("isIdle", true);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Wall")
        {
            GetComponent<AudioSource>().Play();
            GameState.IsGamePaused = true;
            StartCoroutine(waitDie(1.5f));
        }
        else if (other.tag == "Coin")
        {
            GameState.Score += 1;
            Destroy(other.gameObject);
        }
        else if (other.tag == "Exit")
        {
            GameState.IsGamePaused = true;
            StartCoroutine(waitWin(1));
        }
    }
    IEnumerator waitDie(float x)
    {
        yield return waitForSeconds(x);
        m_LooseUI.SetActive(true);
    }
    IEnumerator waitWin(float x)
    {
        yield return waitForSeconds(x);
        m_WinUI.SetActive(true);
    }
    IEnumerator waitForSeconds(float time)
    {
        for (float t = 0.0f; t < time; t += Time.deltaTime)
        {
            yield return 0;
        }
    }
}
