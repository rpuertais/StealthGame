using UnityEngine;
using UnityEngine.UI;

public class UIDistanceAndTime : MonoBehaviour
{
    [Header("Assigna des de l'Inspector")]
    public Transform player;
    public Text distanceText;   
    public Text timeText;       

    private Vector2 lastPos;
    private float totalDistance;
    private float elapsedTime;

    private void Start()
    {
        if (player != null) lastPos = player.position;
        totalDistance = 0f;
        elapsedTime = 0f;

        UpdateUI();
    }

    private void Update()
    {
        if (player == null) return;

        
        elapsedTime += Time.deltaTime;

        
        Vector2 currentPos = player.position;
        totalDistance += Vector2.Distance(currentPos, lastPos);
        lastPos = currentPos;

        UpdateUI();
    }

    private void UpdateUI()
    {
        if (distanceText != null)
            distanceText.text = "Distance: " + totalDistance.ToString("F2") + " units";

        if (timeText != null)
            timeText.text = "Time: " + elapsedTime.ToString("F2") + " sec.";
    }
}

