using UnityEngine;

public class EnemyStatic : MonoBehaviour
{
    [SerializeField]
    private float rot;

    void Update()
    {
        if (transform.rotation.z <= 0 || transform.rotation.z >= 1) 
        {
            rot *= -1;
        } 

        Rotate(rot);
    }

    void Rotate(float rot)
    {
        transform.Rotate(0, 0, rot);
    }
}
