using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [Header("Patrol Points")]
    public Transform PointA;
    public Transform PointB;

    [Header("Visual")]
    [SerializeField] private Transform spriteTransform;   

    [Header("Speeds")]
    [SerializeField] private float patrolSpeed = 2f;
    [SerializeField] private float chaseSpeed = 3f;

    [Header("Rotation")]
    [SerializeField] private int angleRotMov;
    [SerializeField] private int angleRotChase;
    [SerializeField] private float angleFlip1;
    [SerializeField] private float angleFlip2;

    private bool objectivePointA = true;

    private VisionDetector vision;
    private Transform chaseTarget;

    private void Awake()
    {
        vision = GetComponentInChildren<VisionDetector>(true);

        if (spriteTransform == null)
            spriteTransform = transform.Find("Sprite");
    }

    private void Update()
    {
        if (vision != null && vision.DetectedPlayer != null)
        { chaseTarget = vision.DetectedPlayer; }
        else
        { chaseTarget = null; }

        if (chaseTarget != null)
        { Chase(); }
        else
        { Patrol();}
    }

    private void Patrol()
    {
        Vector2 pointPos;

        if (objectivePointA)
        { pointPos = (Vector2)PointA.position; }
        else
        { pointPos = (Vector2)PointB.position; }

        Vector2 dir = pointPos - (Vector2)transform.position;

        if (vision != null) vision.SetForward(dir);
        FaceVertical(dir);

        transform.position = Vector2.MoveTowards(transform.position, pointPos, patrolSpeed * Time.deltaTime);

        if (Vector2.Distance(transform.position, PointA.position) < 0.1f)
        { objectivePointA = false; }

        if (Vector2.Distance(transform.position, PointB.position) < 0.1f)
        { objectivePointA = true; }
    }

    private void Chase()
    {
        Vector2 targetPos = chaseTarget.position;
        Vector2 dir = targetPos - (Vector2)transform.position;

        if (vision != null) vision.SetForward(dir);
        { FaceToPlayer(dir); }

        transform.position = Vector2.MoveTowards(transform.position, targetPos, chaseSpeed * Time.deltaTime);
    }
   
    private void FaceVertical(Vector2 dir)
    {
        if (spriteTransform == null) return;

        float angle = 0f;

        if (dir.y >= 0f)
        { angle = angleFlip1; }
        else
        { angle = angleFlip2; }

        spriteTransform.eulerAngles = new Vector3(0f, 0f, angle + angleRotMov);
    }
    
    private void FaceToPlayer(Vector2 dir)
    {
        if (spriteTransform == null) return;
        if (dir.sqrMagnitude < 0.0001f) return;

        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        spriteTransform.eulerAngles = new Vector3(0f, 0f, angle + angleRotChase);
    }
}


