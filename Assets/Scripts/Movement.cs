using UnityEngine;
using UnityEngine.SceneManagement;

public class Movement : MonoBehaviour
{



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        Death();
    }

    public void Death()
    {
        var bullets = FindObjectsByType<Bullet>(FindObjectsSortMode.None);
        
        foreach (Bullet b in bullets)
        {
            if (b == null) continue;

            float distance = (transform.position - b.transform.position).magnitude;
            if (distance < 4f)
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
