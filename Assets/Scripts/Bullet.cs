using UnityEngine; 

public class Bullet : MonoBehaviour 
{ 
    public enum BulletType { Straight, Homing } 
    public BulletType type = BulletType.Straight; 
    public Vector3 target = Vector3.zero; 
    public float speed = 5f; 
    public Transform targetPosition; 

    void Start() { } 

    void Update() 
    { 
        if (type == BulletType.Homing) 
        { 
            transform.position += transform.right * Time.deltaTime * speed; 
        } 
        else 
        { 
            if(target == Vector3.zero) return; 
            transform.position += target.normalized * Time.deltaTime * speed; 
        } 

        if(type == BulletType.Homing) 
        { 

            if (targetPosition != null) 
            { 
                target = targetPosition.position - transform.position; 
                if (target != Vector3.zero) 
                { 
                    Quaternion targetRotation = Quaternion.LookRotation(Vector3.forward, target);
                    
                    transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 3f * Time.deltaTime); 
                } 
            } 
        } 
    } 
}
