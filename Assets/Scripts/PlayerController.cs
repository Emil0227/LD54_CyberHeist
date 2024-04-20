using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private GameObject m_LooseUI;
    [SerializeField] private GameObject m_WinUI;
    [SerializeField] private GameObject m_Bullet;
    [SerializeField] private GameObject m_CDmask;
    [SerializeField] private GameObject m_BreakerReadyPrompt;

    private Rigidbody2D m_PlayerRigidbody;
    private float m_PlayerSpeedVertical;
    private float m_PlayerSpeedHorizontal;
    private float m_BreakerCD;
    private Animator m_Anim;
    private Timer m_CDTimer;
    private bool m_IsGameStart;
    

    void Start()
    {
        m_CDmask.GetComponent<Image>().fillAmount = 0;
        m_PlayerRigidbody = GetComponent<Rigidbody2D>();
        m_PlayerSpeedVertical = 5;
        m_PlayerSpeedHorizontal = 5;
        m_BreakerCD = 3;
        m_Anim = GetComponent<Animator>();
        m_CDTimer = gameObject.AddComponent<Timer>();
        m_IsGameStart = true;
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

    void Update()
    {
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
            bullet.transform.position = transform.position;
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
