using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    private Transform origin;
    private Transform target;
    private Transform control;

    private float timeTotal = 0f;
    public float duration = 4f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        origin = GameObject.Find("Spawn2").transform;
        target = GameObject.Find("Target").transform;
        control = GameObject.Find("2_Control1").transform;    
    }

    // Update is called once per frame
    void Update()
    {
        if (origin == null || target == null || control == null) return;
        var t = timeTotal/duration;
        if (t > 1f) return;


        transform.position = QuadBezier(origin.transform.position, control.transform.position, target.transform.position, t);
        timeTotal += Time.deltaTime;
        Death();
    }

    private Vector3 QuadBezier(Vector3 p1, Vector3 p2, Vector3 p3, float t)
    {
        float u = 1f - t;
        return u * u * p1 + u * t * p2 + t * t * p3;
    }

    public void Death()
{
    var bullets = FindObjectsByType<Bullet>(FindObjectsSortMode.None);
    
    foreach (Bullet b in bullets)
    {
        if (b == null || b.gameObject == null) continue;

        float distance = (transform.position - b.transform.position).magnitude;
        if (distance < 0.3f)
        {
            if (EnemySpawn.activeEnemies.Contains(gameObject))
            {
                EnemySpawn.activeEnemies.Remove(gameObject);
            }

            Destroy(b.gameObject);
            Destroy(gameObject);

            break; 
        }
    }
}
}
