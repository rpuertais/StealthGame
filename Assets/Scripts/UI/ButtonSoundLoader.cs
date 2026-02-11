using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class UIButtonActions : MonoBehaviour
{
    [SerializeField] private AudioSource clickSound;
    [SerializeField] private float delay = 0.25f;

    public void LoadScene(string sceneName)
    {
        if (clickSound != null)
            clickSound.Play();

        StartCoroutine(LoadAfterDelay(sceneName));
    }

    public void ExitGame()
    {
        if (clickSound != null)
            clickSound.Play();

        StartCoroutine(QuitAfterDelay());
    }

    private IEnumerator LoadAfterDelay(string sceneName)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(sceneName);
    }

    private IEnumerator QuitAfterDelay()
    {
        yield return new WaitForSeconds(delay);
        Application.Quit();
    }
}
