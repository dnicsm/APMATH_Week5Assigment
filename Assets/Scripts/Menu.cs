using UnityEngine;

public class Menu : MonoBehaviour
{
    public GameObject ui;
    public HealthBar health;
    public EnemySpawn enemySpawn;
    
    public bool win = false;

    void Start()
    {
        if (ui != null) ui.SetActive(false);
    }

    void Update()
    {
        if (health == null || health.healthBar == null) return;

        if (win) return;

        if (health.healthBar.fillAmount <= 0f)
        {
            TriggerGameOver();
        }
    }

    void TriggerGameOver()
    {
        win = true;

        if (ui != null) ui.SetActive(true);

        if (enemySpawn != null) 
        {
            enemySpawn.enabled = false;
        }
        else
        {
            Debug.LogWarning("EnemySpawn reference is missing on the Menu script!");
        }

        Rotation[] allRotations = FindObjectsByType<Rotation>(FindObjectsSortMode.None);
        foreach (var rot in allRotations)
        {
            if (rot != null) rot.enabled = false;
        }

        for (int i = EnemySpawn.activeEnemies.Count - 1; i >= 0; i--)
        {
            GameObject enemy = EnemySpawn.activeEnemies[i];
            if (enemy != null)
            {
                Destroy(enemy);
            }
        }

        EnemySpawn.activeEnemies.Clear();
    }
}