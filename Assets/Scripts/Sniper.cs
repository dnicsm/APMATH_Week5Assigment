using UnityEngine;

public class Sniper : MonoBehaviour
{
    public Bullet bulletPrefab;
    public Rotation rotation;
    private Vector3 targetDirection = Vector3.zero;
    public Menu menu;

    void Start()
    {
        menu = FindFirstObjectByType<Menu>();
        rotation = GetComponent<Rotation>();
        
        InvokeRepeating("Shoot", 0, 2f);
    }

    void Update()
    {
        Death();
    }

    void Shoot()
    {
        if (menu.win) return;

        if (rotation.dot >= 0.90f && rotation.dot <= 1.1f && rotation.inDistance)
        {
            Vector3 target = rotation.target.position;
            targetDirection = target - transform.position;
            Quaternion aimRotation = Quaternion.FromToRotation(Vector3.right, targetDirection);

            
            Bullet newBullet = Instantiate(bulletPrefab);
            newBullet.speed = 30f;
            newBullet.transform.position = transform.position;
            newBullet.transform.rotation = aimRotation;
            newBullet.target = targetDirection;

            Destroy(newBullet.gameObject, 3f); 
        }
    }

    public void Death()
    {
        var bullets = FindObjectsByType<Bullet>(FindObjectsSortMode.None);
        
        foreach (Bullet b in bullets)
        {
            if (b == null) continue;

            for (int i = EnemySpawn.activeEnemies.Count - 1; i >= 0; i--)
            {
                GameObject enemy = EnemySpawn.activeEnemies[i];
                if (enemy == null) continue;

                float distance = (enemy.transform.position - b.transform.position).magnitude;
                if (distance < 1f)
                {
                    EnemySpawn.activeEnemies.RemoveAt(i);
                    Destroy(enemy);
                    Destroy(b.gameObject);
                    break;
                }
            }
        }
    }
}