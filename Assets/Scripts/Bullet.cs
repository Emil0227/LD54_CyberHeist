using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float m_Speed;

    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
    void Update()
    {
        transform.Translate(Vector3.up * m_Speed * Time.deltaTime);
    }
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Wall")
        {
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
    }
}
