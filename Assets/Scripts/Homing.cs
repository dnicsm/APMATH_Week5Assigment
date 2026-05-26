using UnityEngine;
using System.Collections.Generic;
public class Homing : MonoBehaviour
{
    public Bullet bulletPrefab;
    public Rotation rotation;
    private Vector3 targetDirection = Vector3.zero;
    public Menu menu;
    public GameObject explosionPrefab;
    private Transform firstTarget;
    private List<Bullet> activeBullets = new List<Bullet>();
    void Start()
    {
        menu = FindFirstObjectByType<Menu>();
        rotation = GetComponent<Rotation>();
        InvokeRepeating("Shoot", 0, 5f);
    }
    void Update()
    {
        if (firstTarget == null && rotation != null && rotation.target != null)
            firstTarget = rotation.target;
        for (int i = activeBullets.Count - 1; i >= 0; i--)
        {
            Bullet b = activeBullets[i];
            if (b == null || firstTarget == null)
            {
                activeBullets.RemoveAt(i);
                continue;
            }
            float dist = (b.transform.position - firstTarget.position).magnitude;
            Debug.Log("Bullet distance to target: " + dist);
            if (dist < 1f)
            {
                GameObject explosion = Instantiate(explosionPrefab, b.transform.position, Quaternion.identity);
                Destroy(explosion, 2f);
                Destroy(b.gameObject);
                activeBullets.RemoveAt(i);
            }
        }
    }
    void Shoot()
    {
        if (menu.win) return;
        if (rotation == null || rotation.target == null) return;
        if (rotation.dot >= 0.20f && rotation.dot <= 1.30f && rotation.inDistance)
        {
            Vector3 target = rotation.target.position;
            targetDirection = target - transform.position;
            Bullet newBullet = Instantiate(bulletPrefab, transform.position, transform.rotation);
            newBullet.type = Bullet.BulletType.Homing;
            newBullet.speed = 10f;
            newBullet.targetPosition = rotation.target;
            newBullet.target = targetDirection;
            activeBullets.Add(newBullet);
            Destroy(newBullet.gameObject, 5f);
        }
    }
}