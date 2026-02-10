using UnityEngine;
using UnityEngine.UI;

public class UIDistanceAndTime : MonoBehaviour
{
    [Header("Assigna des de l'Inspector")]
    public Transform player;
    public Text distanceText;   
    public Text timeText;       

    private Vector2 lastPos;
    public float TotalDistance;
    public float ElapsedTime;

    private void Start()
    {
        if (player != null) lastPos = player.position;
        TotalDistance = 0f;
        ElapsedTime = 0f;

        UpdateUI();
    }

    private void Update()
    {
        if (player == null) return;
        
        ElapsedTime += Time.deltaTime;

        Vector2 currentPos = player.position;
        TotalDistance += Vector2.Distance(currentPos, lastPos);
        lastPos = currentPos;

        UpdateUI();
    }

    private void UpdateUI()
    {
        if (distanceText != null)
            distanceText.text = "Distance: " + TotalDistance.ToString("F2") + " units";

        if (timeText != null)
            timeText.text = "Time: " + ElapsedTime.ToString("F2") + " sec.";
    }
}

