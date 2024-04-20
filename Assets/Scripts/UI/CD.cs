using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CD : MonoBehaviour
{
    private Timer m_CDTimer;

    void Start()
    {
        gameObject.GetComponent<Image>().fillMethod = Image.FillMethod.Radial360;
        m_CDTimer = gameObject.AddComponent<Timer>();
    }

    void Update()
    {
        if (gameObject.GetComponent<Image>().fillAmount > 0.0f)
        {
            gameObject.GetComponent<Image>().fillAmount -= Time.deltaTime * 0.333f;
        }
    }
}
