using UnityEngine;
using System.Collections.Generic;

public class EnemySpawn : MonoBehaviour
{
    [Header("Enemy Prefabs")]
    public GameObject Enemy1;
    public GameObject Enemy2;

    [Header("Spawn & Exit Points")]
    public Transform spawn1;
    public Transform spawn2;
    public Transform exit;

    public HealthBar gameHealthBar;

    public static List<GameObject> activeEnemies = new List<GameObject>();

    void Start()
    {
        InvokeRepeating("SpawnEnemy", 0f, 1.5f);
    }

    void Update()
    {
        List<GameObject> enemies = activeEnemies;
    if (enemies == null || exit == null) return;

    for (int i = enemies.Count - 1; i >= 0; i--)
    {
        GameObject enemy = enemies[i];

        if (enemy == null)
        {
            enemies.RemoveAt(i);
            continue;
        }

        if ((enemy.transform.position - exit.position).magnitude < 1f)
        {
            if (gameHealthBar != null)
            {
                gameHealthBar.TakeDamage(0.05f);
            }

            Destroy(enemy);           
            enemies.RemoveAt(i); 
        }
    }
    }

    void SpawnEnemy()
    {
        GameObject clone1 = Instantiate(Enemy1, spawn1.position, Quaternion.identity);
        activeEnemies.Add(clone1);

        GameObject clone2 = Instantiate(Enemy2, spawn2.position, Quaternion.identity);
        activeEnemies.Add(clone2);
    }

    void OnDisable()
{
    CancelInvoke("SpawnEnemy");
}
}