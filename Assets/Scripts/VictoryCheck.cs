using UnityEngine;
using UnityEngine.SceneManagement;

public class VictoryCheck : MonoBehaviour
{
    public UIDistanceAndTime ScoreSystem;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerPrefs.SetFloat("TotalDistance", ScoreSystem.TotalDistance);
            PlayerPrefs.Save();
            PlayerPrefs.SetFloat("ElapsedTime", ScoreSystem.ElapsedTime);
            PlayerPrefs.Save();
            SceneManager.LoadScene(2);
        }
    }
}
