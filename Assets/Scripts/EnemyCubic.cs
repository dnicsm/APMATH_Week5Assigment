using UnityEngine;

public class EnemyCubic : MonoBehaviour
{
    private Transform origin;
    private Transform target;
    private Transform control;
    private Transform control2;

    private float timeTotal = 0f;
    public float duration = 10f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        origin = GameObject.Find("Spawn1").transform;
        target = GameObject.Find("Target").transform;
        control = GameObject.Find("1_Control1").transform;
        control2 = GameObject.Find("1_Control2").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (origin == null || target == null || control == null || control2 == null) return;
        var t = timeTotal/duration;
        if (t > 1f) return;


        transform.position = CubicBezier(origin.transform.position, control.transform.position, control2.transform.position, target.transform.position, t);
        timeTotal += Time.deltaTime;
        Death();
    }

    private Vector3 CubicBezier(Vector3 p1, Vector3 p2, Vector3 p3, Vector3 p4, float t)
    {
        float u = 1f - t;
        return u * u * u * p1 + 3 * u * u * t * p2 + 3 * u * t * t * p3 + t * t * t * p4;
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
