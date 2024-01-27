// BulletScript.cs

using UnityEngine;

public class BulletScript : MonoBehaviour
{
    public float bulletSpeed = 10f;
    public float destroyTime = 5f;

    private float direction;

    void Start()
    {
        Destroy(gameObject, destroyTime);
    }

    void Update()
    {
        MoveBullet();
    }

    public void SetDirection(float dir)
    {
        direction = dir;
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(direction * bulletSpeed, rb.velocity.y);
    }

    void MoveBullet()
    {
        Vector3 movement = new Vector3(direction, 0f, 0f) * bulletSpeed * Time.deltaTime;
        transform.Translate(movement);
    }
}
