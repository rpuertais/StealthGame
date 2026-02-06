using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAlarm : MonoBehaviour
{
    SpriteRenderer alarmRenderer;
    //VisionDetector detector;

    /*private void Awake()
    {
        detector = GetComponent<VisionDetector>();
    }*/

    /*private void Update()
    {        
        if (detector != null && detector.DetectPlayers().Length > 0)
        {
            PlayerDetected();
        }
        else
        {
            PlayerLeft();
        }
    }*/

    public void PlayerDetected()
    {
        ChangeColor(Color.red);
    }

    public void PlayerLeft()
    {
        ChangeColor(Color.green);
    }

    private void ChangeColor(Color color)
    {
        if (alarmRenderer == null) alarmRenderer = GetComponent<SpriteRenderer>();

        alarmRenderer.color = color;
    }
}
