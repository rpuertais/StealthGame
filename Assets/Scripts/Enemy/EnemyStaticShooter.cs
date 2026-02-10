using UnityEngine;
public class EnemyStaticShooter : MonoBehaviour
{
    [Header("Refs")]
    private VisionDetector vision;
    [SerializeField] private Transform firePoint;

    [Header("Bullet")]
    [SerializeField] private Bullet bulletPrefab;

    [Header("Fire Settings")]
    [SerializeField] private float fireRate = 3f;
    [SerializeField] private float shootRange = 3f;

    private float nextShotTime;

    private void Awake()
    {
            vision = GetComponentInChildren<VisionDetector>(true);
    }

    private void Update()
    {
        if (vision == null || bulletPrefab == null || firePoint == null) return;

        if (vision.DetectedPlayer == null) return;

        float dist = Vector2.Distance(firePoint.position, vision.DetectedPlayer.position);
        if (dist > shootRange) return;

        if (Time.time < nextShotTime) return;

        Shoot();
        nextShotTime = Time.time + (1f / fireRate);
    }

    private void Shoot()
    {
        Vector2 dir = vision.Forward2D;
        if (dir.sqrMagnitude < 0.0001f)
            dir = (vision.DetectedPlayer.position - firePoint.position).normalized;

        Bullet b = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        b.Init(dir);
    }
}
