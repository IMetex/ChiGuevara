using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 10f;
    private bool isJumping = false;
    private int jumpCount = 0;
    public GameObject bulletPrefab; // Ateş objesi prefab'ı
    public float minYPosition = -5f; // Alt sınır

    private bool canFire = true; // Fire yapma izni kontrolü

    void Update()
    {
        // Sağa ve sola hareket
        float horizontalInput = Input.GetAxis("Horizontal");
        Vector2 moveDirection = new Vector2(horizontalInput, 0f);
        transform.Translate(moveDirection * moveSpeed * Time.deltaTime);

        // Ateş etme
        if (Input.GetKey(KeyCode.Space))
        {
            // Yatay gidişatı belirle (sağa veya sola)
            float bulletDirection = 0f;
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                bulletDirection = -1f; // Sola ateş et
            }
            else if (Input.GetKey(KeyCode.RightArrow))
            {
                bulletDirection = 1f; // Sağa ateş et
            }

            if (bulletDirection != 0f && canFire)
            {
                Fire(bulletDirection);
                canFire = false; // Fire yapıldığında izni kapat

                // 2 saniye sonra izni tekrar aç
                Invoke("EnableFire", 1f);
            }
        }

        // Havalanma
        if (Input.GetKeyDown(KeyCode.UpArrow) && jumpCount < 2)
        {
            Jump();
            jumpCount++;
        }

        // Alt sınıra ulaşıldığında düşmeyi engelleme
        if (transform.position.y < minYPosition)
        {
            transform.position = new Vector2(transform.position.x, minYPosition);
            if (isJumping)
            {
                isJumping = false;
                jumpCount = 0;
            }
        }
    }

    void Jump()
    {
        GetComponent<Rigidbody2D>().velocity = new Vector2(0f, jumpForce);
        isJumping = true;
    }

    void Fire(float bulletDirection)
    {
        // Ateş objesini oyuncunun altında oluştur
        Vector2 spawnPosition = new Vector2(transform.position.x, Mathf.Max(transform.position.y - 1f, minYPosition));

        GameObject bullet = Instantiate(bulletPrefab, spawnPosition, Quaternion.identity);
        BulletScript bulletScript = bullet.GetComponent<BulletScript>();

        // Ateş objesini belirli bir yönde hareket ettir
        bulletScript.SetDirection(bulletDirection);
    }

    void EnableFire()
    {
        canFire = true; // Fire iznini aç
    }

    // Zeminle temas ettiğinde
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Graund"))
        {
            isJumping = false;
            jumpCount = 0;
        }
    }
}
