using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 10f;
    private bool isJumping = false;
    private int jumpCount = 0;
    public GameObject bulletPrefab; // Ateş objesi prefab'ı
    public float bulletLifetime = 1f; // Ateş objesinin ömrü

    public float minYPosition = -5f; // Alt sınır
    public float destroyYPosition = -1f; // Ateş objesinin yok olacağı y pozisyonu

    void Update()
    {
        // Sağa ve sola hareket
        float horizontalInput = Input.GetAxis("Horizontal");
        Vector2 moveDirection = new Vector2(horizontalInput, 0f);
        transform.Translate(moveDirection * moveSpeed * Time.deltaTime);

        // Ateş etme
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Fire();
        }

        // Havalanma
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (jumpCount < 2)
            {
                Jump();
                jumpCount++;
            }
        }

        if (Input.GetKeyUp(KeyCode.UpArrow))
        {
            isJumping = false;
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
        if (!isJumping)
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(0f, jumpForce);
            isJumping = true;
        }
    }

    void Fire()
    {
        // Ateş objesini oyuncunun altında oluştur
        Vector2 spawnPosition = new Vector2(transform.position.x, Mathf.Max(transform.position.y - 1f, minYPosition));
        GameObject bullet = Instantiate(bulletPrefab, spawnPosition, Quaternion.identity);

        // Ateş objesinin belirli bir süre sonra yok olması için Invoke fonksiyonu kullanılır
        Invoke("DestroyBullet", bulletLifetime);
    }

    // Zeminle temas ettiğinde ateş objesini yok et
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            DestroyBullet(collision.gameObject);
            print("buldu");
            jumpCount = 0; // Yere temas ettiğinde jumpCount sıfırlanır
        }
    }

    // Ateş objesini yok etme fonksiyonu
    public void DestroyBullet(GameObject bullet)
    {
        // Eğer obje hala varsa ve y pozisyonu destroyYPosition'dan küçükse, objeyi yok et
        if (bullet != null && bullet.transform.position.y < destroyYPosition)
        {
            Destroy(bullet);
        }
    }
}
