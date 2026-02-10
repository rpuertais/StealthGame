using UnityEngine;
using UnityEngine.UI;

public class UIDistanceAndTime : MonoBehaviour
{
    [Header("Assigna des de l'Inspector")]
    public Transform Player;
    public Text DistanceText;   
    public Text TimeText;       

    private Vector2 lastPos;
    public float TotalDistance;
    public float ElapsedTime;

    private void Start()
    {
        if (Player != null) lastPos = Player.position;
        TotalDistance = 0f;
        ElapsedTime = 0f;

        UpdateUI();
    }

    private void Update()
    {
        if (Player == null) return;
        
        ElapsedTime += Time.deltaTime;

        Vector2 currentPos = Player.position;
        TotalDistance += Vector2.Distance(currentPos, lastPos);
        lastPos = currentPos;

        UpdateUI();
    }

    private void UpdateUI()
    {
        if (DistanceText != null)
            DistanceText.text = "Distance: " + TotalDistance.ToString("F2") + " units";

        if (TimeText != null)
            TimeText.text = "Time: " + ElapsedTime.ToString("F2") + " sec.";
    }
}

