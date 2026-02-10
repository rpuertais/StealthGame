

using System.Collections.Generic;
using UnityEngine;

public class VisionDetector : MonoBehaviour
{
    [Header("Detection")]
    public LayerMask WhatIsPlayer;
    public LayerMask WhatIsVisible;
    public float DetectionRange = 4f;
    public float VisionAngle = 90f;

    [Header("AlarmUI")]
    public AlarmUI AlarmUI;

    private bool isDetecting = false;


    public Transform DetectedPlayer { get; private set; }
    
    public Vector2 Forward2D { get; private set; } = Vector2.right;

    public void SetForward(Vector2 dir)
    {
        if (dir.sqrMagnitude < 0.0001f) return;
        Forward2D = dir.normalized;
    }

    private void Update()
    {
        var detected = DetectPlayers();
        bool nowDetecting = detected.Length > 0;

        if (nowDetecting)
        {
            DetectedPlayer = detected[0];
        }
        else
        {
            DetectedPlayer = null;
        }

        if (!isDetecting && nowDetecting)
        {
            AlarmUI.PlayerDetected();
        }
        else if (isDetecting && !nowDetecting)
        {
            AlarmUI.PlayerLeft();
        }

        isDetecting = nowDetecting;
    }

    public Transform[] DetectPlayers()
    {
        List<Transform> players = new List<Transform>();

        if (PlayerInRange(ref players))
        {
            if (PlayerInAngle(ref players))
            {
                PlayerIsVisible(ref players);
            }
        }

        return players.ToArray();
    }

    private bool PlayerInRange(ref List<Transform> players)
    {
        Collider2D[] playerColliders = Physics2D.OverlapCircleAll(transform.position, DetectionRange, WhatIsPlayer);

        if (playerColliders.Length == 0) return false;

        foreach (var item in playerColliders)
            players.Add(item.transform);

        return true;
    }

    private bool PlayerInAngle(ref List<Transform> players)
    {
        for (int i = players.Count - 1; i >= 0; i--)
        {
            float angle = GetAngle(players[i]);
            if (angle > VisionAngle * 0.5f)
                players.RemoveAt(i);
        }

        return (players.Count > 0);
    }

    private float GetAngle(Transform target)
    {
        Vector2 targetDir = (target.position - transform.position).normalized;
        return Vector2.Angle(targetDir, Forward2D);
    }

    private bool PlayerIsVisible(ref List<Transform> players)
    {
        for (int i = players.Count - 1; i >= 0; i--)
        {
            if (!IsVisible(players[i]))
                players.RemoveAt(i);
        }

        return (players.Count > 0);
    }

    private bool IsVisible(Transform target)
    {
        Vector3 dir = target.position - transform.position;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, dir.normalized, DetectionRange, WhatIsVisible);

        return (hit.collider != null && hit.collider.transform == target);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(transform.position, DetectionRange);
        
        Vector2 f = Forward2D.sqrMagnitude < 0.0001f ? Vector2.right : Forward2D.normalized;
        float halfRad = (VisionAngle * 0.5f) * Mathf.Deg2Rad;

        Vector2 left = Rotate2D(f, +halfRad);
        Vector2 right = Rotate2D(f, -halfRad);

        Gizmos.color = Color.yellow;
        Gizmos.DrawRay(transform.position, (Vector3)(left * DetectionRange));
        Gizmos.DrawRay(transform.position, (Vector3)(right * DetectionRange));

        Gizmos.color = Color.white;
    }


    private Vector2 Rotate2D(Vector2 v, float radians)
    {
        float cos = Mathf.Cos(radians);
        float sin = Mathf.Sin(radians);
        return new Vector2(v.x * cos - v.y * sin, v.x * sin + v.y * cos);
    }
}

