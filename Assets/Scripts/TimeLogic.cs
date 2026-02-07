using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class TimeLogic : MonoBehaviour
{
    private Text label;
    private float totalTime; //guarda el temps total que ha passat des que ha començat la partida

    private void Awake()
    {
        label = GetComponent<Text>();
    }

    private void Update()
    {
        totalTime += Time.deltaTime;
        UpdateTimeText(totalTime);
    }

    private void UpdateTimeText(float time)
    {
        int seconds = Mathf.FloorToInt(time); // passar de float a int
        label.text = "Time: " + seconds + " sec.";
    }
}
