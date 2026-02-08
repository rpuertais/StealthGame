using UnityEngine;

public class EnemyStatic : MonoBehaviour
{

    private float timer = 2.0f;
    private int rot;

    void Start()
    {
        rot = 90;    
    }

    void Update()
    {
        timer -= Time.deltaTime;
        if (timer < 0)
        {
            if (transform.rotation.z == 0 || transform.rotation.z == 1) 
            {
                rot *= -1;
            } 

            Rotate(rot);
            timer = 2.0f;
        }

    }

    void Rotate(int rot)
    {
        transform.Rotate(0, 0, rot);
    }
}
