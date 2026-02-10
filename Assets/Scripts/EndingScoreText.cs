using TMPro;
using UnityEngine;

public class EndingScoreText : MonoBehaviour
{
    public TextMeshProUGUI ScoreText;

    void Start()
    {
        float distance = 0f;
        float timer = 0f;
        float totalScore = distance / (timer / 10);

        ScoreText.text = "Distance: " + distance + "\nTimer: " + timer + "\nTotalScore: " + totalScore;
    }
}
