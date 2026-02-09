using UnityEngine;
using UnityEngine.UI;

public class EnemyAlarm : MonoBehaviour
{

    public GameObject RedAlarm;


    public void PlayerDetected()
    {
        Debug.Log("isActive");
        ChangeColor(true);
    }

    public void PlayerLeft()
    {
        ChangeColor(false);
    }

    private void ChangeColor(bool isActive)
    {
        Debug.Log(isActive);
        RedAlarm.SetActive(isActive);
    }
}
