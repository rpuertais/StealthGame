using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerDeath : MonoBehaviour
{
    [SerializeField] private string gameOverSceneName = "Death";
    
    public UIDistanceAndTime ScoreSystem;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Bullet"))
        {
            Die();
            return;
        }
        
        if (other.CompareTag("Enemy"))
        {
            Die();
            return;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Enemy"))
        {
            Die();
            return;
        }
    }

    private void Die()
    {
        PlayerPrefs.SetFloat("TotalDistance", ScoreSystem.TotalDistance);
        PlayerPrefs.Save();
        PlayerPrefs.SetFloat("ElapsedTime", ScoreSystem.ElapsedTime);
        PlayerPrefs.Save();
        SceneManager.LoadScene(gameOverSceneName);
    }
}
