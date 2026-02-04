using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public Transform PointA;
    public Transform PointB;

    [SerializeField]
    private float speed = 2f;

    private bool objectivePointA;

    void Start()
    {
        objectivePointA = true;
    }

    void Update()
    {
        Move();

        if (Vector2.Distance(transform.position, PointA.position) < 0.1f)
        {
            Flip();
            objectivePointA = false;
        }

        if (Vector2.Distance(transform.position, PointB.position) < 0.1f)
        {
            Flip();
            objectivePointA = true;
        }
    }

    private void Move()
    {
        Vector2 pointPos; 

        if (objectivePointA) pointPos = PointA.position; 
        else pointPos = PointB.position;

        transform.position = Vector2.MoveTowards( transform.position, pointPos, speed * Time.deltaTime );
    }

    private void Flip()
    {
        transform.Rotate(0, 180, 0);
    }

}
