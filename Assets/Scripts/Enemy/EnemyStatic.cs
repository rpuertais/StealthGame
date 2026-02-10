using UnityEngine;
public class EnemyStatic : MonoBehaviour
{
    private enum State { Patrol, Aim, ReturnToPatrol }

    [Header("Patrol (Swing)")]
    [SerializeField] private float patrolSpeedDegPerSec = 60f; 
    [SerializeField] private float maxSwingAngle = 90f;         

    [Header("Aim (Chase)")]
    [SerializeField] private float aimSpeedDegPerSec = 90f;     
    [SerializeField] private float loseTargetGraceTime = 0.35f; 

    [Header("Return")]
    [SerializeField] private float returnSpeedDegPerSec = 90f;  

     private VisionDetector vision;             
     private Transform rotatePivot;             

    private State state = State.Patrol;

    private float baseAngle;      
    private float currentOffset;  
    private int swingDir = 1;

    private Transform currentTarget;
    private Vector2 lastKnownTargetPos;
    private float graceTimer = 0f;

    private void Awake()
    {
        rotatePivot = transform;
        vision = GetComponentInChildren<VisionDetector>(true);

        baseAngle = NormalizeAngle(rotatePivot.eulerAngles.z);
        currentOffset = 0f;

        UpdateVisionForwardFromAngle(baseAngle);
    }

    private void Update()
    {
        if (vision != null && vision.DetectedPlayer != null)
        {
            currentTarget = vision.DetectedPlayer;
            lastKnownTargetPos = currentTarget.position;
            graceTimer = loseTargetGraceTime;
        }
        else
        {
            graceTimer -= Time.deltaTime;
            if (graceTimer <= 0f)
                currentTarget = null;
        }

        switch (state)
        {
            case State.Patrol:

                PatrolRotate();

                if (currentTarget != null && IsPosWithinSwing(lastKnownTargetPos))
                    state = State.Aim;
                break;

            case State.Aim:
                
                if (!IsPosWithinSwing(lastKnownTargetPos))
                {
                    state = State.ReturnToPatrol;
                    break;
                }

                
                AimToPosition(lastKnownTargetPos);

                if (currentTarget == null)
                {
                    state = State.ReturnToPatrol;
                }
                break;

            case State.ReturnToPatrol:

                if (ReturnToSwingBand())
                {
                    state = State.Patrol;
                }
                break;
        }
    }

    private void PatrolRotate()
    {
        float half = maxSwingAngle * 0.5f;

        currentOffset += swingDir * patrolSpeedDegPerSec * Time.deltaTime;

        if (currentOffset >= half)
        {
            currentOffset = half;
            swingDir = -1;
        }
        else if (currentOffset <= -half)
        {
            currentOffset = -half;
            swingDir = 1;
        }

        SetPivotAngle(baseAngle + currentOffset);
    }

    private void AimToPosition(Vector2 targetPos)
    {
        Vector2 toTarget = targetPos - (Vector2)rotatePivot.position;
        if (toTarget.sqrMagnitude < 0.0001f) return;

        float targetAngle = Mathf.Atan2(toTarget.y, toTarget.x) * Mathf.Rad2Deg;
        targetAngle = NormalizeAngle(targetAngle);

        float currentAngle = NormalizeAngle(rotatePivot.eulerAngles.z);
        float newAngle = Mathf.MoveTowardsAngle(currentAngle, targetAngle, aimSpeedDegPerSec * Time.deltaTime);

        SetPivotAngle(newAngle);
    }
    
    private bool ReturnToSwingBand()
    {
        float half = maxSwingAngle * 0.5f;
        float currentAngle = NormalizeAngle(rotatePivot.eulerAngles.z);

        bool inside = IsAngleWithinBand(currentAngle, baseAngle, half);
        if (inside)
        {
            currentOffset = Mathf.DeltaAngle(baseAngle, currentAngle);
            swingDir = (currentOffset >= 0f) ? -1 : 1;
            return true;
        }

        float targetA = NormalizeAngle(baseAngle - half);
        float targetB = NormalizeAngle(baseAngle + half);

        float da = Mathf.Abs(Mathf.DeltaAngle(currentAngle, targetA));
        float db = Mathf.Abs(Mathf.DeltaAngle(currentAngle, targetB));

        float target = (da <= db) ? targetA : targetB;

        float newAngle = Mathf.MoveTowardsAngle(currentAngle, target, returnSpeedDegPerSec * Time.deltaTime);
        SetPivotAngle(newAngle);

        return false;
    }

    private bool IsPosWithinSwing(Vector2 pos)
    {
        Vector2 toPos = pos - (Vector2)rotatePivot.position;
        if (toPos.sqrMagnitude < 0.0001f) return true;

        float angleToPos = Mathf.Atan2(toPos.y, toPos.x) * Mathf.Rad2Deg;
        angleToPos = NormalizeAngle(angleToPos);

        float delta = Mathf.Abs(Mathf.DeltaAngle(baseAngle, angleToPos));
        return (delta <= maxSwingAngle * 0.5f);
    }

    private bool IsAngleWithinBand(float angle, float center, float half)
    {
        float delta = Mathf.Abs(Mathf.DeltaAngle(center, angle));
        return delta <= half + 0.0001f;
    }
    
    private void SetPivotAngle(float angleDeg)
    {
        rotatePivot.eulerAngles = new Vector3(0f, 0f, angleDeg);
        UpdateVisionForwardFromAngle(angleDeg);
    }

    private void UpdateVisionForwardFromAngle(float angleDeg)
    {
        if (vision == null) return;

        float rad = angleDeg * Mathf.Deg2Rad;
        Vector2 forward = new Vector2(Mathf.Cos(rad), Mathf.Sin(rad));
        vision.SetForward(forward);
    }

    private float NormalizeAngle(float angle)
    {
        angle %= 360f;
        if (angle < 0f) angle += 360f;
        return angle;
    }
}
