using UnityEngine;
using UnityEngine.UI;

public class AlarmUI : MonoBehaviour
{
    [SerializeField] private Image alarmImage;

    private void Awake()
    {
        if (alarmImage == null)
            alarmImage = GetComponent<Image>(); 

        if (alarmImage != null)
            alarmImage.color = Color.green;
    }

    public void PlayerDetected()
    {
        ChangeColor(Color.red); 
    }

    public void PlayerLeft()
    {
        ChangeColor(Color.green);
    }

    private void ChangeColor(Color color)
    {
        if (alarmImage == null) return;
        alarmImage.color = color;
    }
}
