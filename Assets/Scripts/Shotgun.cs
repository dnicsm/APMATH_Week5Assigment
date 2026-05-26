using UnityEngine;
using System.Collections.Generic;

public class Shotgun : MonoBehaviour
{
    public Bullet bulletPrefab;
    public Rotation rotation;
    public float angle;

    private List<Bullet> bullets = new List<Bullet>();
    public Menu menu;

    private Vector3 targetDirection = Vector3.zero;

    void Start()
    {
        rotation = GetComponent<Rotation>();   
        menu = FindAnyObjectByType<Menu>();     
        InvokeRepeating("Shoot", 0, 2f);
    }

    void Update()
    {
        if (bullets.Count == 0) return;
        
        float size = (bullets[0].transform.position - transform.position).magnitude;

        if (size > 6f)
        {
            Debug.Log($"Size: {size}");
            DestroyBullets();
        }
    }

    void Shoot()
    {   
        if (menu.win) return;
        
        if (rotation.dot >= 0.70f && rotation.dot <= 1.30f && rotation.inDistance)
        {
            float centerAngle = rotation.angle;
            for (int i = 0; i < 5; i++)
            {
                angle = centerAngle + (i - 2) * (rotation.cone / 5f) * 2.5f;

                Vector3 target = new Vector3(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad), 0);

                Bullet bulletInstance = Instantiate(bulletPrefab);
                bulletInstance.speed = 20f;
                bulletInstance.transform.position = transform.position;
                bulletInstance.transform.rotation = Quaternion.Euler(0, 0, 90f);                
                bulletInstance.target = target;
                bullets.Add(bulletInstance);
            }
        }
    }

    void DestroyBullets()
    {
        foreach (Bullet b in bullets)
        {
            if (b != null && b.gameObject != null)
            {
                Destroy(b.gameObject);
            }
        }
        bullets.Clear();
    }
}