using Unity.VisualScripting;
using UnityEngine;
using System.Collections.Generic;

public class Fire : MonoBehaviour
{
    public Bullet bulletPrefab;

    public Rotation rotation;
    private Vector3 targetDirection = Vector3.zero;

    public Sprite fireSprites;

    public float angle;
    public float speed = 5f;

    public float LineAngle = 70f;
    public float length = 15f;
    public int resolution = 40;

    private Menu menu;

    private LineRenderer lr;

    private List<Bullet> bullets = new List<Bullet>();

    Bullet bullet;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        menu = FindObjectOfType<Menu>();
        rotation = GetComponent<Rotation>();      
        float adjustLength = rotation.inRange / transform.localScale.x;

        lr = gameObject.AddComponent<LineRenderer>();
        lr.startWidth = 0.01f;
        lr.endWidth = 0.01f;
        lr.material = new Material(Shader.Find("Unlit/Color"));
        lr.material.color = Color.black;
        lr.loop = true;
        lr.useWorldSpace = false;
        lr.sortingLayerName = "Player";
        SetCone(rotation.cone * 2f, adjustLength);  

        DrawCone();
        InvokeRepeating("Shoot", 0, 2f);
        
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = bullets.Count - 1; i >= 0; i--)
        {
            if (bullets[i] == null)
            {
                bullets.RemoveAt(i);
            }
        }

        if (bullets.Count == 0) return;

        DrawCone();

        Bullet oldestBullet = bullets[0];
        
        if (oldestBullet != null)
        {
            float size = (oldestBullet.transform.position - transform.position).magnitude;

            if (size > 5f)
            {
                Debug.Log($"Size: {size}");
                DestroyBullets();
            }
        }

    }

    void Shoot()
    {   
            // Debug.Log($"Dot: {rotation.dot}");
            if (menu.win)
            {return;}
            if (rotation.dot >= 0.70f && rotation.dot <= 1.30f && rotation.inDistance)
            {
                float centerAngle = rotation.angle;
                for(int i = 0; i < 4; i++)
                {
                angle = centerAngle + (i - 2) * (rotation.cone / 4f);
                // Debug.Log($"Angle: {angle}");
                // Debug.Log("Shooting");

                Vector3 target = new Vector3(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad), 0);
                Bullet bullet = Instantiate(bulletPrefab);
                bullet.speed = 10f;

                bullet.GetComponent<SpriteRenderer>().sprite = fireSprites;
                bullet.GetComponent<SpriteRenderer>().flipY = true;
                bullet.GetComponent<SpriteRenderer>().color = Color.white;
                bullet.GetComponent<SpriteRenderer>().transform.localScale = new Vector3(0.1f, 0.1f, 1f);
                bullet.transform.position = transform.position;
                bullet.target = target;
                bullets.Add(bullet);
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

    void DrawCone()
    {
        lr.positionCount = resolution + 2;

        lr.SetPosition(0, Vector3.zero);

        float halfAngle = LineAngle / 2f;
        for (int i = 0; i <= resolution; i++)
        {
            float a = Mathf.Deg2Rad * (-halfAngle + (LineAngle / resolution) * i);
            Vector3 point = new Vector3(Mathf.Cos(a), Mathf.Sin(a), 0) * length;
            lr.SetPosition(i + 1, point);
        }
    }

    public void SetCone(float angle, float length)
    {
        this.LineAngle = angle;
        this.length = length;
    }
}
