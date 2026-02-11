using TMPro;
using UnityEngine;

public class EndingScoreText : MonoBehaviour
{
    public TextMeshProUGUI ScoreText;

    void Start()
    {
        float distance = PlayerPrefs.GetFloat("TotalDistance");
        float timer = PlayerPrefs.GetFloat("ElapsedTime");
        float totalScore = distance / (timer / 10);

        ScoreText.text = "Distance: " + distance + "\nTimer: " + timer + "\nTotalScore: " + totalScore;
    }
}
