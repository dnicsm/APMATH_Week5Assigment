using UnityEngine;
using System.Collections.Generic;

public class Rotation : MonoBehaviour
{
    public Transform target;
    public float inRange = 4f;
    public float cone = 45f;
    public float dot = 0f;
    public float angle = 0f;

    private Quaternion defaultR;

    public bool TargetHit;
    public bool inDistance;

    public float spinAngle = 0f;
    public float spinSpeed = 90f;

    public float speed = 5f;

    void Start()
    {
        defaultR = transform.rotation;
    }

    void Update()
    {
        List<GameObject> enemies = EnemySpawn.activeEnemies;
        target = null;
        float closestDistance = 0f;

        if (enemies != null && enemies.Count > 0)
        {
            for (int i = 0; i < enemies.Count; i++)
            {
                if (enemies[i] != null)
                {
                    target = enemies[i].transform;
                    closestDistance = (target.position - transform.position).magnitude;
                    break; 
                }
            }

            if (target != null)
            {
                foreach (GameObject enemy in enemies)
                {
                    if (enemy == null) continue;

                    float distanceToEnemy = (enemy.transform.position - transform.position).magnitude;
                    if (distanceToEnemy < closestDistance)
                    {
                        closestDistance = distanceToEnemy;
                        target = enemy.transform;
                    }
                }
            }
        }

        if (target == null) return;

        var direction = target.position - transform.position;
        angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        var distance = direction.magnitude;
        dot = Vector3.Dot(transform.right.normalized, direction.normalized);
        TargetHit = Mathf.Abs(Mathf.DeltaAngle(transform.eulerAngles.z, angle)) <= cone;
        inDistance = distance < inRange;
        
        if (inDistance && TargetHit)
        {
            var Rotate = Quaternion.Euler(0, 0, angle);
            transform.rotation = Quaternion.Slerp(transform.rotation, Rotate, Time.deltaTime * speed);
        }
        else
        {
            spinAngle += spinSpeed * Time.deltaTime;
            float half = Mathf.Sin(spinAngle * Mathf.Deg2Rad) * 90f;
            var Rotate = Quaternion.Euler(0, 0, defaultR.eulerAngles.z + half);
            transform.rotation = Quaternion.Slerp(transform.rotation, Rotate, Time.deltaTime);
        }
    }
}